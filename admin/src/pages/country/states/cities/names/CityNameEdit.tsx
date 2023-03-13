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
import { AlertBox } from "../../../../../utility/Alerts";
import LanguageDropdown from "../../../../../dropdowns/LanguageDropdown";
import { LanguageRes } from "../../../../../dtos/Language";
import { CityNameReqEdit } from "../../../../../dtos/CityName";
import { CityNamesApi } from "../../../../../api/cityNamesApi";
import { CityApi } from "../../../../../api/cityApi";
import { CityRes } from "../../../../../dtos/City";

const CityNameEdit = () => {
  const params = useParams();
  const cityNameId = params.cityNameId;
  const cityId = params.cityId;
  const updateText = cityNameId ? "Update Name" : "Add Name";
  // console.log("person id: " + personId)
  // console.log(updateText)
  const [cityName, setCityName] = useState<CityNameReqEdit>(new CityNameReqEdit(cityId));
  const [city, setCity] = useState<CityRes>();
  const toast = useToast();
  const navigate = useNavigate();
  const [error, setError] = useState("");

  const [selectedLanguage, setSelectedLanguage] = useState<LanguageRes>();

  useEffect(() => {
    loadCityName();
    loadCity();
  }, [cityNameId]);

  const loadCityName = () => {
    setError("");
    if (cityNameId) {
      CityNamesApi.get(cityNameId)
        .then((res) => {
          setCityName(res);
          setSelectedLanguage(res.language)
        })
        .catch((error) => {
          setError(error.response.data.error);
          toast({
            title: "Failed to get City Name",
            description: error.response.data.error,
            status: "error",
            position: "bottom-right",
          });
        });
    }
  };

  const loadCity = () => {
    CityApi.get(cityId).then(res => {
      setCity(res);
    })
  }

  // Formik validation schema
  const validationSchema = Yup.object({
    name: Yup.string().required("Name is required"),
    languageId: Yup.number().required("Select Language").min(1),
    cityId: Yup.number().required().min(1),
  });

  const submitForm = (values: CityNameReqEdit) => {
    // console.log(values);
    if (cityNameId) {
      updateCityName(values);
    } else {
      createCityName(values);
    }
  };

  const updateCityName = (values: CityNameReqEdit) => {
    setError("");
    CityNamesApi.update(cityNameId, values)
      .then((res) => {
        toast({
          title: "Success",
          description: "City Name updated successfully.",
          status: "success",
          position: "bottom-right",
        });
        navigate(-1);
      })
      .catch((error) => {
        setError(error.response.data.error);
      });
  };

  const createCityName = (values: CityNameReqEdit) => {
    setError("");
    CityNamesApi.create(values)
      .then((res) => {
        toast({
          title: "Success",
          description: "City Name created successfully.",
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
        initialValues={cityName}
        onSubmit={(values) => {
          submitForm(values);
        }}
        validationSchema={validationSchema}
        enableReinitialize={true}
      >
        {({ handleSubmit, errors, touched, setFieldValue }) => (
          <form onSubmit={handleSubmit}>
            <Stack spacing={4} as={Container} maxW={"3xl"}>
              <FormControl isInvalid={!!errors.name && touched.name}>
                <FormLabel fontSize={"sm"} htmlFor="name">Name</FormLabel>
                <Field size={"sm"} as={Input} id="name" name="name" type="text" />
                <Field size={"sm"} as={Input} id="cityId" name="cityId" type="hidden" />
                <FormErrorMessage>{errors.name}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.languageId && touched.languageId}>
                <FormLabel size={"sm"} htmlFor="languageId">Language</FormLabel>
                <Field size={"sm"} as={Input} id="languageId" name="languageId" type="hidden"
                />
                <LanguageDropdown
                  selectedLanguage={selectedLanguage}
                  handleChange={(newValue?: LanguageRes) => {
                    setSelectedLanguage(newValue);
                    setFieldValue("languageId", newValue?.languageId || "");
                  }}
                />
                <FormErrorMessage>{errors.languageId}</FormErrorMessage>
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
        <Heading fontSize={"lg"}>
          {updateText} - 
          {city?.slug + ", " + city?.state?.slug + ", " + city?.state?.country?.slug}
        </Heading>
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
    <Box width={"2xl"} p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        {displayHeading()}
        {error && <AlertBox description={error} />}
        {showUpdateForm()}
      </Stack>
    </Box>
  );
}

export default CityNameEdit