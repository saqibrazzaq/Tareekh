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
import { StateNamesApi } from "../../../../api/stateNamesApi";
import { StateNameRes } from "../../../../dtos/StateName";
import { AlertBox } from "../../../../utility/Alerts";

const StateNameDelete = () => {
  let params = useParams();
  const stateNameId = params.stateNameId;
  const [stateName, setStateName] = useState<StateNameRes>();
  const navigate = useNavigate();
  const toast = useToast();
  const [error, setError] = useState("");

  const { isOpen, onOpen, onClose } = useDisclosure();
  const cancelRef = React.useRef<HTMLAnchorElement>(null);

  useEffect(() => {
    loadStateName();
  }, [stateNameId]);

  const loadStateName = () => {
    setError("")
    if (stateNameId) {
      StateNamesApi.get(stateNameId)
        .then((res) => {
          setStateName(res);
          // console.log(res);
        })
        .catch((error) => {
          setError(error.response.data.error);
          console.log("Error in api: " + error);
        });
    }
  };

  const deleteStateName = () => {
    setError("")
    StateNamesApi.delete(stateNameId).then(res => {
      toast({
        title: "Success",
        description: stateName?.name + " deleted successfully.",
        status: "success",
        position: "bottom-right",
      });
      navigate(-1);
    }).catch(error => {
      console.log("Error " + error)
      setError(error.response.data.error);
      toast({
        title: "Error deleting State Name",
        description: error.response.data.error,
        status: "error",
        position: "bottom-right",
      });
    })
  }

  const displayHeading = () => (
    <Flex>
      <Box>
        <Heading fontSize={"xl"}>Delete State Name</Heading>
      </Box>
      <Spacer />
      <Box>
        <Button type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
          Back
        </Button>
      </Box>
    </Flex>
  );

  const showStateNameInfo = () => (
    <div>
      <Text fontSize="xl">
        Are you sure you want to delete the following State Name?
      </Text>
      <TableContainer>
        <Table variant="simple">
          <Tbody>
            <Tr>
              <Th>Name</Th>
              <Td>
                {stateName?.name}
              </Td>
            </Tr>
            <Tr>
              <Th>State Slug</Th>
              <Td>{stateName?.state?.slug + ", " + stateName?.state?.country?.slug}</Td>
            </Tr>
            <Tr>
              <Th>Language</Th>
              <Td>{stateName?.language?.name}</Td>
            </Tr>
          </Tbody>
        </Table>
      </TableContainer>
      <HStack pt={4} spacing={4}>
        <Button onClick={onOpen} type="button" colorScheme={"red"}>
          YES, I WANT TO DELETE THIS STATE NAME
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
            Delete State Name
          </AlertDialogHeader>

          <AlertDialogBody>
            Are you sure? You can't undo this action afterwards.
          </AlertDialogBody>

          <AlertDialogFooter>
            <Link ref={cancelRef} onClick={onClose}>
              <Button type="button" colorScheme={"gray"}>Cancel</Button>
            </Link>
            <Link onClick={deleteStateName} ml={3}>
              <Button type="submit" colorScheme={"red"}>Delete State Name</Button>
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
        {showStateNameInfo()}
        {showAlertDialog()}
      </Stack>
    </Box>
  );
}

export default StateNameDelete