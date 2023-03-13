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
import { CountryApi } from "../../api/countryApi";
import { CountryNamesApi } from "../../api/countryNamesApi";
import { StateApi } from "../../api/stateApi";
import { CountryRes } from "../../dtos/Country";
import { AlertBox } from "../../utility/Alerts";

const CountryDelete = () => {
  let params = useParams();
  const countryId = params.countryId;
  const [country, setCountry] = useState<CountryRes>();
  const [stateCount, setStateCount] = useState(0);
  const [countryNameCount, setCountryNameCount] = useState(0);
  const navigate = useNavigate();
  const toast = useToast();
  const [error, setError] = useState("");

  const { isOpen, onOpen, onClose } = useDisclosure();
  const cancelRef = React.useRef<HTMLAnchorElement>(null);

  useEffect(() => {
    loadCountry();
    loadStateCount();
    loadCountryNameCount();
  }, [countryId]);

  const loadCountry = () => {
    setError("")
    if (countryId) {
      CountryApi.get(countryId)
        .then((res) => {
          setCountry(res);
          // console.log(res);
        })
        .catch((error) => {
          setError(error.response.data.error);
          console.log("Error in api: " + error);
        });
    }
  };

  const loadCountryNameCount = () => {
    CountryNamesApi.count(countryId).then(res => {
      setCountryNameCount(res);
    })
  }

  const loadStateCount = () => {
    StateApi.countByCountryId(countryId).then(res => {
      setStateCount(res);
    })
  }

  const deleteCountry = () => {
    setError("")
    CountryApi.delete(countryId).then(res => {
      toast({
        title: "Success",
        description: country?.slug + " deleted successfully.",
        status: "success",
        position: "bottom-right",
      });
      navigate(-1);
    }).catch(error => {
      console.log("Error " + error)
      setError(error.response.data.error);
      toast({
        title: "Error deleting Country",
        description: error.response.data.error,
        status: "error",
        position: "bottom-right",
      });
    })
  }

  const displayHeading = () => (
    <Flex>
      <Box>
        <Heading fontSize={"xl"}>Delete Country</Heading>
      </Box>
      <Spacer />
      <Box>
        <Button type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
          Back
        </Button>
      </Box>
    </Flex>
  );

  const showCountryInfo = () => (
    <div>
      <Text fontSize="xl">
        Are you sure you want to delete the following Country?
      </Text>
      <TableContainer>
        <Table variant="simple">
          <Tbody>
            <Tr>
              <Th>Name</Th>
              <Td>
                {country?.slug}
              </Td>
            </Tr>
            <Tr>
              <Th>State Count</Th>
              <Td>{stateCount}</Td>
            </Tr>
          </Tbody>
        </Table>
      </TableContainer>
      {(stateCount > 0) && <AlertBox title="Cannot Delete Country" description={"It has states."} />}
      {(countryNameCount > 0) && <AlertBox title="Cannot Delete Country" description={"It has names."} />}
      <HStack pt={4} spacing={4}>
        <Button onClick={onOpen} type="button" colorScheme={"red"}>
          YES, I WANT TO DELETE THIS COUNTRY
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
            Delete Country
          </AlertDialogHeader>

          <AlertDialogBody>
            Are you sure? You can't undo this action afterwards.
          </AlertDialogBody>

          <AlertDialogFooter>
            <Link ref={cancelRef} onClick={onClose}>
              <Button type="button" colorScheme={"gray"}>Cancel</Button>
            </Link>
            <Link onClick={deleteCountry} ml={3}>
              <Button type="submit" colorScheme={"red"}>Delete Country</Button>
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
        {showCountryInfo()}
        {showAlertDialog()}
      </Stack>
    </Box>
  );
}

export default CountryDelete