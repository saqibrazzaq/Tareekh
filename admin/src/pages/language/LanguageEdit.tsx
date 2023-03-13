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
import { LanguageReqEdit } from "../../dtos/Language";
import { LanguageApi } from "../../api/languageApi";

const LanguageEdit = () => {
  const params = useParams();
  const languageId = params.languageId;
  const updateText = languageId ? "Update Language" : "Add Language";
  // console.log("person id: " + personId)
  // console.log(updateText)
  const [language, setLanguage] = useState<LanguageReqEdit>(new LanguageReqEdit());
  const toast = useToast();
  const navigate = useNavigate();
  const [error, setError] = useState("");

  useEffect(() => {
    loadLanguage();
  }, [languageId]);

  const loadLanguage = () => {
    setError("");
    if (languageId) {
      LanguageApi.get(languageId)
        .then((res) => {
          setLanguage(res);
        })
        .catch((error) => {
          setError(error.response.data.error);
          toast({
            title: "Failed to get Language",
            description: error.response.data.error,
            status: "error",
            position: "bottom-right",
          });
        });
    }
  };

  // Formik validation schema
  const validationSchema = Yup.object({
    name: Yup.string().required("Name is required"),
    languageCode: Yup.string().required(),
  });

  const submitForm = (values: LanguageReqEdit) => {
    // console.log(values);
    if (languageId) {
      updateLanguage(values);
    } else {
      createLanguage(values);
    }
  };

  const updateLanguage = (values: LanguageReqEdit) => {
    setError("");
    LanguageApi.update(languageId, values)
      .then((res) => {
        toast({
          title: "Success",
          description: "Language updated successfully.",
          status: "success",
          position: "bottom-right",
        });
        navigate(-1);
      })
      .catch((error) => {
        setError(error.response.data.error);
      });
  };

  const createLanguage = (values: LanguageReqEdit) => {
    setError("");
    LanguageApi.create(values)
      .then((res) => {
        toast({
          title: "Success",
          description: "Language created successfully.",
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
        initialValues={language}
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
                <FormErrorMessage>{errors.name}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!errors.languageCode && touched.languageCode}>
                <FormLabel fontSize={"sm"} htmlFor="languageCode">Language Code</FormLabel>
                <Field size={"sm"} as={Input} id="languageCode" name="languageCode" type="text" />
                <FormErrorMessage>{errors.languageCode}</FormErrorMessage>
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

export default LanguageEdit