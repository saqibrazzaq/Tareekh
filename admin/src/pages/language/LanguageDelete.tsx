import {
  AlertDialog,
  AlertDialogBody,
  AlertDialogContent,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogOverlay,
  Box,
  Button,
  Container,
  Flex,
  Heading,
  HStack,
  Link,
  Spacer,
  Stack,
  Table,
  TableContainer,
  Tbody,
  Td,
  Text,
  Th,
  Tr,
  useDisclosure,
  useToast,
} from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import { useParams, Link as RouteLink, useNavigate } from "react-router-dom";
import { CityNamesApi } from "../../api/cityNamesApi";
import { CountryNamesApi } from "../../api/countryNamesApi";
import { LanguageApi } from "../../api/languageApi";
import { StateNamesApi } from "../../api/stateNamesApi";
import { LanguageRes } from "../../dtos/Language";
import { AlertBox } from "../../utility/Alerts";

const LanguageDelete = () => {
  let params = useParams();
  const languageId = params.languageId;
  const [language, setLanguage] = useState<LanguageRes>();
  const [anyCountryByLanguage, setAnyCountryByLanguage] = useState(false);
  const [anyStateByLanguage, setAnyStateByLanguage] = useState(false);
  const [anyCityByLanguage, setAnyCityByLanguage] = useState(false);
  const navigate = useNavigate();
  const toast = useToast();
  const [error, setError] = useState("");

  const { isOpen, onOpen, onClose } = useDisclosure();
  const cancelRef = React.useRef<HTMLAnchorElement>(null);

  useEffect(() => {
    loadLanguage();
    loadCountryNameCount();
    loadCityNameCount();
    loadStateNameCount();
  }, [languageId]);

  const loadStateNameCount = () => {
    StateNamesApi.anyByLanguage(languageId).then(res => {
      setAnyStateByLanguage(res);
    })
  }

  const loadCityNameCount = () => {
    CityNamesApi.anyByLanguage(languageId).then(res => {
      setAnyCityByLanguage(res);
    })
  }

  const loadCountryNameCount = () => {
    CountryNamesApi.anyByLanguage(languageId).then(res => {
      setAnyCountryByLanguage(res);
    })
  }

  const loadLanguage = () => {
    setError("")
    if (languageId) {
      LanguageApi.get(languageId)
        .then((res) => {
          setLanguage(res);
          // console.log(res);
        })
        .catch((error) => {
          setError(error.response.data.error);
          console.log("Error in api: " + error);
        });
    }
  };

  const deleteLanguage = () => {
    setError("")
    LanguageApi.delete(languageId).then(res => {
      toast({
        title: "Success",
        description: language?.name + " deleted successfully.",
        status: "success",
        position: "bottom-right",
      });
      navigate(-1);
    }).catch(error => {
      console.log("Error " + error)
      setError(error.response.data.error);
      toast({
        title: "Error deleting Language",
        description: error.response.data.error,
        status: "error",
        position: "bottom-right",
      });
    })
  }

  const displayHeading = () => (
    <Flex>
      <Box>
        <Heading fontSize={"xl"}>Delete Language</Heading>
      </Box>
      <Spacer />
      <Box>
        <Button type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
          Back
        </Button>
      </Box>
    </Flex>
  );

  const showCountryNameInfo = () => (
    <div>
      <Text fontSize="xl">
        Are you sure you want to delete the following Language?
      </Text>
      <TableContainer>
        <Table variant="simple">
          <Tbody>
            <Tr>
              <Th>Name</Th>
              <Td>
                {language?.name}
              </Td>
            </Tr>
            <Tr>
              <Th>Code</Th>
              <Td>{language?.languageCode}</Td>
            </Tr>
          </Tbody>
        </Table>
      </TableContainer>
      {(anyCountryByLanguage) && <AlertBox title="Cannot Delete Language" description={"It is used in Countries."} />}
      {(anyStateByLanguage) && <AlertBox title="Cannot Delete Language" description={"It is used in States."} />}
      {(anyCityByLanguage) && <AlertBox title="Cannot Delete Language" description={"It is used in Cities."} />}
      <HStack pt={4} spacing={4}>
        <Button onClick={onOpen} type="button" colorScheme={"red"}>
          YES, I WANT TO DELETE THIS LANGUAGE
        </Button>
      </HStack>
    </div>
  );

  const showAlertDialog = () => (
    <AlertDialog
      isOpen={isOpen}
      leastDestructiveRef={cancelRef}
      onClose={onClose}
    >
      <AlertDialogOverlay>
        <AlertDialogContent>
          <AlertDialogHeader fontSize="lg" fontWeight="bold">
            Delete Language
          </AlertDialogHeader>

          <AlertDialogBody>
            Are you sure? You can't undo this action afterwards.
          </AlertDialogBody>

          <AlertDialogFooter>
            <Link ref={cancelRef} onClick={onClose}>
              <Button type="button" colorScheme={"gray"}>Cancel</Button>
            </Link>
            <Link onClick={deleteLanguage} ml={3}>
              <Button type="submit" colorScheme={"red"}>Delete Language</Button>
            </Link>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialogOverlay>
    </AlertDialog>
  );

  return (
    <Box p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        {displayHeading()}
        {error && <AlertBox title="Error" description={error} />}
        {showCountryNameInfo()}
        {showAlertDialog()}
      </Stack>
    </Box>
  );
}

export default LanguageDelete