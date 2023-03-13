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
import { CityNamesApi } from "../../../../../api/cityNamesApi";
import { CityNameRes } from "../../../../../dtos/CityName";
import { AlertBox } from "../../../../../utility/Alerts";

const CityNameDelete = () => {
  let params = useParams();
  const cityNameId = params.cityNameId;
  const [cityName, setCityName] = useState<CityNameRes>();
  const navigate = useNavigate();
  const toast = useToast();
  const [error, setError] = useState("");

  const { isOpen, onOpen, onClose } = useDisclosure();
  const cancelRef = React.useRef<HTMLAnchorElement>(null);

  useEffect(() => {
    loadCityName();
  }, [cityNameId]);

  const loadCityName = () => {
    setError("")
    if (cityNameId) {
      CityNamesApi.get(cityNameId)
        .then((res) => {
          setCityName(res);
          // console.log(res);
        })
        .catch((error) => {
          setError(error.response.data.error);
          console.log("Error in api: " + error);
        });
    }
  };

  const deleteCityName = () => {
    setError("")
    CityNamesApi.delete(cityNameId).then(res => {
      toast({
        title: "Success",
        description: cityName?.name + " deleted successfully.",
        status: "success",
        position: "bottom-right",
      });
      navigate(-1);
    }).catch(error => {
      console.log("Error " + error)
      setError(error.response.data.error);
      toast({
        title: "Error deleting City Name",
        description: error.response.data.error,
        status: "error",
        position: "bottom-right",
      });
    })
  }

  const displayHeading = () => (
    <Flex>
      <Box>
        <Heading fontSize={"xl"}>Delete City Name</Heading>
      </Box>
      <Spacer />
      <Box>
        <Button type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
          Back
        </Button>
      </Box>
    </Flex>
  );

  const showCityNameInfo = () => (
    <div>
      <Text fontSize="xl">
        Are you sure you want to delete the following City Name?
      </Text>
      <TableContainer>
        <Table variant="simple">
          <Tbody>
            <Tr>
              <Th>Name</Th>
              <Td>
                {cityName?.name}
              </Td>
            </Tr>
            <Tr>
              <Th>City Slug</Th>
              <Td>{cityName?.city?.slug + ", " + cityName?.city?.state?.slug + 
                ", " + cityName?.city?.state?.country?.slug}</Td>
            </Tr>
            <Tr>
              <Th>Language</Th>
              <Td>{cityName?.language?.name}</Td>
            </Tr>
          </Tbody>
        </Table>
      </TableContainer>
      <HStack pt={4} spacing={4}>
        <Button onClick={onOpen} type="button" colorScheme={"red"}>
          YES, I WANT TO DELETE THIS CITY NAME
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
            Delete City Name
          </AlertDialogHeader>

          <AlertDialogBody>
            Are you sure? You can't undo this action afterwards.
          </AlertDialogBody>

          <AlertDialogFooter>
            <Link ref={cancelRef} onClick={onClose}>
              <Button type="button" colorScheme={"gray"}>Cancel</Button>
            </Link>
            <Link onClick={deleteCityName} ml={3}>
              <Button type="submit" colorScheme={"red"}>Delete City Name</Button>
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
        {showCityNameInfo()}
        {showAlertDialog()}
      </Stack>
    </Box>
  );
}

export default CityNameDelete