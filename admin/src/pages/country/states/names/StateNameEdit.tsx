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
import LanguageDropdown from "../../../../dropdowns/LanguageDropdown";
import { LanguageRes } from "../../../../dtos/Language";
import { StateNameReqEdit } from "../../../../dtos/StateName";
import { StateNamesApi } from "../../../../api/stateNamesApi";

const StateNameEdit = () => {
  const params = useParams();
  const stateNameId = params.stateNameId;
  const stateId = params.stateId;
  const updateText = stateNameId ? "Update Name" : "Add Name";
  // console.log("person id: " + personId)
  // console.log(updateText)
  const [stateName, setStateName] = useState<StateNameReqEdit>(new StateNameReqEdit(stateId));
  const toast = useToast();
  const navigate = useNavigate();
  const [error, setError] = useState("");

  const [selectedLanguage, setSelectedLanguage] = useState<LanguageRes>();

  useEffect(() => {
    loadStateName();
  }, [stateNameId]);

  const loadStateName = () => {
    setError("");
    if (stateNameId) {
      StateNamesApi.get(stateNameId)
        .then((res) => {
          setStateName(res);
          setSelectedLanguage(res.language)
        })
        .catch((error) => {
          setError(error.response.data.error);
          toast({
            title: "Failed to get State Name",
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
    languageId: Yup.number().required("Select Language").min(1),
    stateId: Yup.number().required().min(1),
  });

  const submitForm = (values: StateNameReqEdit) => {
    // console.log(values);
    if (stateNameId) {
      updateStateName(values);
    } else {
      createStateName(values);
    }
  };

  const updateStateName = (values: StateNameReqEdit) => {
    setError("");
    StateNamesApi.update(stateNameId, values)
      .then((res) => {
        toast({
          title: "Success",
          description: "State Name updated successfully.",
          status: "success",
          position: "bottom-right",
        });
        navigate(-1);
      })
      .catch((error) => {
        setError(error.response.data.error);
      });
  };

  const createStateName = (values: StateNameReqEdit) => {
    setError("");
    StateNamesApi.create(values)
      .then((res) => {
        toast({
          title: "Success",
          description: "State Name created successfully.",
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
        initialValues={stateName}
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
                <Field size={"sm"} as={Input} id="stateId" name="stateId" type="hidden" />
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

export default StateNameEdit