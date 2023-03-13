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
import { CityApi } from "../../../api/cityApi";
import { StateApi } from "../../../api/stateApi";
import { StateNamesApi } from "../../../api/stateNamesApi";
import { StateRes } from "../../../dtos/State";
import { AlertBox } from "../../../utility/Alerts";

const StateDelete = () => {
  let params = useParams();
  const stateId = params.stateId;
  const [state, setState] = useState<StateRes>();
  const [cityCount, setStateCount] = useState(0);
  const [cityNameCount, setCityNameCount] = useState(0);
  const navigate = useNavigate();
  const toast = useToast();
  const [error, setError] = useState("");

  const { isOpen, onOpen, onClose } = useDisclosure();
  const cancelRef = React.useRef<HTMLAnchorElement>(null);

  useEffect(() => {
    loadState();
    loadCityCount();
    loadStateNameCount();
  }, [stateId]);

  const loadState = () => {
    setError("")
    if (stateId) {
      StateApi.get(stateId)
        .then((res) => {
          setState(res);
          // console.log(res);
        })
        .catch((error) => {
          setError(error.response.data.error);
          console.log("Error in api: " + error);
        });
    }
  };

  const loadStateNameCount = () => {
    StateNamesApi.count(stateId).then(res => {
      setCityNameCount(res);
    })
  }

  const loadCityCount = () => {
    CityApi.countByState(stateId).then(res => {
      setStateCount(res);
    })
  }

  const deleteState = () => {
    setError("")
    StateApi.delete(stateId).then(res => {
      toast({
        title: "Success",
        description: state?.slug + " deleted successfully.",
        status: "success",
        position: "bottom-right",
      });
      navigate(-1);
    }).catch(error => {
      console.log("Error " + error)
      setError(error.response.data.error);
      toast({
        title: "Error deleting State",
        description: error.response.data.error,
        status: "error",
        position: "bottom-right",
      });
    })
  }

  const displayHeading = () => (
    <Flex>
      <Box>
        <Heading fontSize={"xl"}>Delete State</Heading>
      </Box>
      <Spacer />
      <Box>
        <Button type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
          Back
        </Button>
      </Box>
    </Flex>
  );

  const showStateInfo = () => (
    <div>
      <Text fontSize="xl">
        Are you sure you want to delete the following State?
      </Text>
      <TableContainer>
        <Table variant="simple">
          <Tbody>
            <Tr>
              <Th>Name</Th>
              <Td>
                {state?.slug}
              </Td>
            </Tr>
            <Tr>
              <Th>Country</Th>
              <Td>{state?.country?.slug}</Td>
            </Tr>
            <Tr>
              <Th>City Count</Th>
              <Td>{cityCount}</Td>
            </Tr>
          </Tbody>
        </Table>
      </TableContainer>
      {(cityCount > 0) && <AlertBox title="Cannot Delete State" description={"It has cities."} />}
      {(cityNameCount > 0) && <AlertBox title="Cannot Delete State" description={"It has names."} />}
      <HStack pt={4} spacing={4}>
        <Button onClick={onOpen} type="button" colorScheme={"red"}>
          YES, I WANT TO DELETE THIS STATE
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
            Delete State
          </AlertDialogHeader>

          <AlertDialogBody>
            Are you sure? You can't undo this action afterwards.
          </AlertDialogBody>

          <AlertDialogFooter>
            <Link ref={cancelRef} onClick={onClose}>
              <Button type="button" colorScheme={"gray"}>Cancel</Button>
            </Link>
            <Link onClick={deleteState} ml={3}>
              <Button type="submit" colorScheme={"red"}>Delete State</Button>
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
        {showStateInfo()}
        {showAlertDialog()}
      </Stack>
    </Box>
  );
}

export default StateDelete