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
import { AlertBox } from "../../../utility/Alerts";
import { StateReqEdit, StateRes } from "../../../dtos/State";
import { StateApi } from "../../../api/stateApi";

const StateEdit = () => {
  const params = useParams();
  const stateId = params.stateId;
  const countryId = params.countryId;
  const updateText = stateId ? "Update State" : "Add State";
  // console.log("person id: " + personId)
  // console.log(updateText)
  const [countryName, setCountryName] = useState<StateReqEdit>(new StateReqEdit(countryId));
  const toast = useToast();
  const navigate = useNavigate();
  const [error, setError] = useState("");

  useEffect(() => {
    loadState();
  }, [stateId]);

  const loadState = () => {
    setError("");
    if (stateId) {
      StateApi.get(stateId)
        .then((res) => {
          setCountryName(res);
        })
        .catch((error) => {
          setError(error.response.data.error);
          toast({
            title: "Failed to get State",
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
    countryId: Yup.number().required().min(1),
  });

  const submitForm = (values: StateReqEdit) => {
    // console.log(values);
    if (stateId) {
      updateState(values);
    } else {
      createState(values);
    }
  };

  const updateState = (values: StateReqEdit) => {
    setError("");
    StateApi.update(stateId, values)
      .then((res) => {
        toast({
          title: "Success",
          description: "State updated successfully.",
          status: "success",
          position: "bottom-right",
        });
        navigate(-1);
      })
      .catch((error) => {
        setError(error.response.data.error);
      });
  };

  const createState = (values: StateReqEdit) => {
    setError("");
    StateApi.create(values)
      .then((res) => {
        toast({
          title: "Success",
          description: "State created successfully.",
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
        initialValues={countryName}
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
                <Field size={"sm"} as={Input} id="countryId" name="countryId" type="hidden" />
                <FormErrorMessage>{errors.slug}</FormErrorMessage>
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

export default StateEdit