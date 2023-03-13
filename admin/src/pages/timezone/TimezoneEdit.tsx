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
import { AlertBox } from "../../utility/Alerts";
import { TimezoneReqEdit } from "../../dtos/Timezone";
import { TimezoneApi } from "../../api/timezoneApi";

const TimezoneEdit = () => {
  const params = useParams();
  const timezoneId = params.timezoneId;
  const updateText = timezoneId ? "Update Time Zone" : "Add Time Zone";
  // console.log("person id: " + personId)
  // console.log(updateText)
  const [timezone, setTimezone] = useState<TimezoneReqEdit>(new TimezoneReqEdit());
  const toast = useToast();
  const navigate = useNavigate();
  const [error, setError] = useState("");

  useEffect(() => {
    loadTimezone();
  }, [timezoneId]);

  const loadTimezone = () => {
    setError("");
    if (timezoneId) {
      TimezoneApi.get(timezoneId)
        .then((res) => {
          setTimezone(res);
        })
        .catch((error) => {
          setError(error.response.data.error);
          toast({
            title: "Failed to get Time Zone",
            description: error.response.data.error,
            status: "error",
            position: "bottom-right",
          });
        });
    }
  };

  // Formik validation schema
  const validationSchema = Yup.object({
    tzName: Yup.string().required("TZ Name is required"),
    cityName: Yup.string().required(),
    gmtOffset: Yup.number().required(),
    gmtOffsetName: Yup.string().required(),
    abbreviation: Yup.string().required(),
  });

  const submitForm = (values: TimezoneReqEdit) => {
    // console.log(values);
    if (timezoneId) {
      updateTimezone(values);
    } else {
      createTimezone(values);
    }
  };

  const updateTimezone = (values: TimezoneReqEdit) => {
    setError("");
    TimezoneApi.update(timezoneId, values)
      .then((res) => {
        toast({
          title: "Success",
          description: "Time Zone updated successfully.",
          status: "success",
          position: "bottom-right",
        });
        navigate(-1);
      })
      .catch((error) => {
        setError(error.response.data.error);
      });
  };

  const createTimezone = (values: TimezoneReqEdit) => {
    setError("");
    TimezoneApi.create(values)
      .then((res) => {
        toast({
          title: "Success",
          description: "Time Zone created successfully.",
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
        initialValues={timezone}
        onSubmit={(values) => {
          submitForm(values);
        }}
        validationSchema={validationSchema}
        enableReinitialize={true}
      >
        {({ handleSubmit, errors, touched, setFieldValue }) => (
          <form onSubmit={handleSubmit}>
            <Stack spacing={4} as={Container} maxW={"3xl"}>
              <FormControl isInvalid={!!errors.tzName && touched.tzName}>
                <FormLabel fontSize={"sm"} htmlFor="tzName">TZ Name</FormLabel>
                <Field size={"sm"} as={Input} id="tzName" name="tzName" type="text" />
                <FormErrorMessage>{errors.tzName}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.cityName && touched.cityName}>
                <FormLabel fontSize={"sm"} htmlFor="cityName">City Name</FormLabel>
                <Field size={"sm"} as={Input} id="cityName" name="cityName" type="text" />
                <FormErrorMessage>{errors.cityName}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.gmtOffset && touched.gmtOffset}>
                <FormLabel fontSize={"sm"} htmlFor="gmtOffset">GMT Offset</FormLabel>
                <Field size={"sm"} as={Input} id="gmtOffset" name="gmtOffset" type="text" />
                <FormErrorMessage>{errors.gmtOffset}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.gmtOffsetName && touched.gmtOffsetName}>
                <FormLabel fontSize={"sm"} htmlFor="gmtOffsetName">GMT Offset Name</FormLabel>
                <Field size={"sm"} as={Input} id="gmtOffsetName" name="gmtOffsetName" type="text" />
                <FormErrorMessage>{errors.gmtOffsetName}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.abbreviation && touched.abbreviation}>
                <FormLabel fontSize={"sm"} htmlFor="abbreviation">Abbreviation</FormLabel>
                <Field size={"sm"} as={Input} id="abbreviation" name="abbreviation" type="text" />
                <FormErrorMessage>{errors.abbreviation}</FormErrorMessage>
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

export default TimezoneEdit