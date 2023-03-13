import {
  Box,
  Button,
  Container,
  Flex,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Heading,
  Input,
  Link,
  Spacer,
  Stack,
  useToast,
} from "@chakra-ui/react";
import { useState, useEffect } from "react";
import { Link as RouteLink, useNavigate, useParams } from "react-router-dom";
import * as Yup from "yup";
import { Field, Formik } from "formik";
import { AlertBox } from "../../../../utility/Alerts";
import { StateReqEdit, StateRes } from "../../../../dtos/State";
import { StateApi } from "../../../../api/stateApi";
import { CityReqEdit } from "../../../../dtos/City";
import { CityApi } from "../../../../api/cityApi";
import TimezoneDropdown from "../../../../dropdowns/TimezoneDropdown";
import { TimezoneRes } from "../../../../dtos/Timezone";

const CityEdit = () => {
  const params = useParams();
  const cityId = params.cityId;
  const stateId = params.stateId;
  const updateText = cityId ? "Update City" : "Add City";
  // console.log("person id: " + personId)
  // console.log(updateText)
  const [city, setCity] = useState<CityReqEdit>(new CityReqEdit(stateId));
  const [selectedTimezone, setSelectedTimezone] = useState<TimezoneRes>();
  const toast = useToast();
  const navigate = useNavigate();
  const [error, setError] = useState("");

  useEffect(() => {
    loadCity();
  }, [cityId]);

  const loadCity = () => {
    setError("");
    if (cityId) {
      CityApi.get(cityId)
        .then((res) => {
          setCity(res);
          setSelectedTimezone(res.timezone)
        })
        .catch((error) => {
          setError(error.response.data.error);
          toast({
            title: "Failed to get City",
            description: error.response.data.error,
            status: "error",
            position: "bottom-right",
          });
        });
    }
  };

  // Formik validation schema
  const validationSchema = Yup.object({
    slug: Yup.string().required("Name is required"),
    stateId: Yup.number().required().min(1),
    latitude: Yup.number(),
    longitude: Yup.number(),
    timezoneId: Yup.number().required("Timezone is required").min(1),
  });

  const submitForm = (values: CityReqEdit) => {
    // console.log(values);
    if (cityId) {
      updateCity(values);
    } else {
      createCity(values);
    }
  };

  const updateCity = (values: CityReqEdit) => {
    setError("");
    CityApi.update(cityId, values)
      .then((res) => {
        toast({
          title: "Success",
          description: "City updated successfully.",
          status: "success",
          position: "bottom-right",
        });
        navigate(-1);
      })
      .catch((error) => {
        setError(error.response.data.error);
      });
  };

  const createCity = (values: CityReqEdit) => {
    setError("");
    CityApi.create(values)
      .then((res) => {
        toast({
          title: "Success",
          description: "City created successfully.",
          status: "success",
          position: "bottom-right",
        });
        navigate(-1);
      })
      .catch((error) => {
        setError(error.response.data.error);
      });
  };

  const showUpdateForm = () => (
    <Box p={0}>
      <Formik
        initialValues={city}
        onSubmit={(values) => {
          submitForm(values);
        }}
        validationSchema={validationSchema}
        enableReinitialize={true}
      >
        {({ handleSubmit, errors, touched, setFieldValue }) => (
          <form onSubmit={handleSubmit}>
            <Stack spacing={4} as={Container} maxW={"3xl"}>
              <FormControl isInvalid={!!errors.slug && touched.slug}>
                <FormLabel fontSize={"sm"} htmlFor="slug">Slug</FormLabel>
                <Field size={"sm"} as={Input} id="slug" name="slug" type="text" />
                <Field size={"sm"} as={Input} id="stateId" name="stateId" type="hidden" />
                <FormErrorMessage>{errors.slug}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.latitude && touched.latitude}>
                <FormLabel fontSize={"sm"} htmlFor="latitude">Latitude</FormLabel>
                <Field size={"sm"} as={Input} id="latitude" name="latitude" type="text" />
                <FormErrorMessage>{errors.latitude}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.longitude && touched.longitude}>
                <FormLabel fontSize={"sm"} htmlFor="longitude">Longitude</FormLabel>
                <Field size={"sm"} as={Input} id="longitude" name="longitude" type="text" />
                <FormErrorMessage>{errors.longitude}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.timezoneId && touched.timezoneId}>
                <FormLabel size={"sm"} htmlFor="timezoneId">Time Zone</FormLabel>
                <Field size={"sm"} as={Input} id="timezoneId" name="timezoneId" type="hidden"
                />
                <TimezoneDropdown
                  selectedTimezone={selectedTimezone}
                  handleChange={(newValue?: TimezoneRes) => {
                    setSelectedTimezone(newValue);
                    setFieldValue("timezoneId", newValue?.timezoneId || "");
                  }}
                />
                <FormErrorMessage>{errors.timezoneId}</FormErrorMessage>
              </FormControl>
              <Stack direction={"row"} spacing={6}>
                <Button size={"sm"} type="submit" colorScheme={"blue"}>
                  {updateText}
                </Button>
              </Stack>
            </Stack>
          </form>
        )}
      </Formik>
    </Box>
  );

  const displayHeading = () => (
    <Flex>
      <Box>
        <Heading fontSize={"lg"}>{updateText}</Heading>
      </Box>
      <Spacer />
      <Box>
        <Button size={"sm"} type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
          Back
        </Button>
      </Box>
    </Flex>
  );

  return (
    <Box width={"lg"} p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        {displayHeading()}
        {error && <AlertBox description={error} />}
        {showUpdateForm()}
      </Stack>
    </Box>
  );
}

export default CityEdit