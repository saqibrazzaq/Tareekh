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
import { CountryApi } from "../../api/countryApi";
import { CountryReqEdit } from "../../dtos/Country";
import * as Yup from "yup";
import { Field, Formik } from "formik";
import { AlertBox } from "../../utility/Alerts";

const CountryEdit = () => {
  const params = useParams();
  const countryId = params.countryId;
  const updateText = countryId ? "Update Country" : "Add Country";
  // console.log("person id: " + personId)
  // console.log(updateText)
  const [country, setCountry] = useState<CountryReqEdit>(new CountryReqEdit());
  const toast = useToast();
  const navigate = useNavigate();
  const [error, setError] = useState("");

  useEffect(() => {
    loadCountry();
  }, [countryId]);

  const loadCountry = () => {
    setError("");
    if (countryId) {
      CountryApi.get(countryId)
        .then((res) => {
          setCountry(res);
        })
        .catch((error) => {
          setError(error.response.data.error);
          toast({
            title: "Failed to get Country",
            description: error.response.data.error,
            status: "error",
            position: "bottom-right",
          });
        });
    }
  };

  // Formik validation schema
  const validationSchema = Yup.object({
    slug: Yup.string().required("Slug is required"),
  });

  const submitForm = (values: CountryReqEdit) => {
    // console.log(values);
    if (countryId) {
      updateCountry(values);
    } else {
      createCountry(values);
    }
  };

  const updateCountry = (values: CountryReqEdit) => {
    setError("");
    CountryApi.update(countryId, values)
      .then((res) => {
        toast({
          title: "Success",
          description: "Country updated successfully.",
          status: "success",
          position: "bottom-right",
        });
        navigate("/countries");
      })
      .catch((error) => {
        setError(error.response.data.error);
      });
  };

  const createCountry = (values: CountryReqEdit) => {
    setError("");
    CountryApi.create(values)
      .then((res) => {
        toast({
          title: "Success",
          description: "Country created successfully.",
          status: "success",
          position: "bottom-right",
        });
        navigate("/countries");
      })
      .catch((error) => {
        setError(error.response.data.error);
      });
  };

  const showUpdateForm = () => (
    <Box p={0}>
      <Formik
        initialValues={country}
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
                <FormLabel htmlFor="slug">Slug</FormLabel>
                <Field as={Input} id="slug" name="slug" type="text" />
                <FormErrorMessage>{errors.slug}</FormErrorMessage>
              </FormControl>
              <Stack direction={"row"} spacing={6}>
                <Button type="submit" colorScheme={"blue"}>
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
        <Heading fontSize={"xl"}>{updateText}</Heading>
      </Box>
      <Spacer />
      <Box>
        <Button type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
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

export default CountryEdit