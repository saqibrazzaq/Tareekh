import {
  Box,
  Button,
  Center,
  Container,
  Flex,
  Heading,
  Input,
  Link,
  Spacer,
  Stack,
  Table,
  TableContainer,
  Tbody,
  Td,
  Tfoot,
  Th,
  Thead,
  Tr,
} from "@chakra-ui/react";
import React, { useEffect, useState } from "react";
import {
  Link as RouteLink,
  useLocation,
  useParams,
  useSearchParams,
} from "react-router-dom";
import Common from "../../utility/Common";
import UpdateIcon from "../../components/icons/UpdateIcon";
import DeleteIcon from "../../components/icons/DeleteIcon";
import PagedRes from "../../dtos/PagedRes";
import { LanguageRes } from "../../dtos/Language";
import { LanguageApi } from "../../api/languageApi";

const Languages = () => {
  const location = useLocation();
  const [searchParams, setSearchParams] = useSearchParams(location.search);
  searchParams.set("pageSize", Common.DEFAULT_PAGE_SIZE.toString());

  const [pagedRes, setPagedRes] = useState<PagedRes<LanguageRes>>();
  const [searchText, setSearchText] = useState<string>("");

  useEffect(() => {
    searchLanguages();
  }, [searchParams]);

  const searchLanguages = () => {
    if (!searchParams) return;
    LanguageApi.search(Object.fromEntries(searchParams)).then((res) => {
      setPagedRes(res);
      // console.log(res)
    });
  };

  const updateSearchParams = (key: string, value: string) => {
    searchParams.set(key, value);
    setSearchParams(searchParams);
  };

  const previousPage = () => {
    if (pagedRes?.metaData) {
      let previousPageNumber = (pagedRes?.metaData?.currentPage || 2) - 1;
      updateSearchParams("pageNumber", previousPageNumber.toString());
    }
  };

  const nextPage = () => {
    if (pagedRes?.metaData) {
      let nextPageNumber = (pagedRes?.metaData?.currentPage || 0) + 1;
      updateSearchParams("pageNumber", nextPageNumber.toString());
    }
  };

  const showHeading = () => (
    <Flex>
      <Box>
        <Heading fontSize={"lg"}>Language List</Heading>
      </Box>
      <Spacer />
      <Box>
        <Link ml={2} as={RouteLink} to={"/languages/edit"}>
          <Button colorScheme={"blue"} size={"sm"}>
            Add Language
          </Button>
        </Link>
      </Box>
    </Flex>
  );

  const displaySearchBar = () => (
    <Flex>
      <Center></Center>
      <Box flex={1} ml={4}></Box>

      <Box ml={4}>
        <Input
          size={"sm"}
          placeholder="Search..."
          value={searchText}
          onChange={(e) => setSearchText(e.currentTarget.value)}
          onKeyDown={(e) => {
            if (e.key === "Enter") {
              updateSearchParams("searchText", searchText);
              updateSearchParams("pageNumber", "1");
            }
          }}
        />
      </Box>
      <Box ml={0}>
        <Button
          colorScheme={"blue"}
          size={"sm"}
          onClick={() => {
            updateSearchParams("searchText", searchText);
            updateSearchParams("pageNumber", "1");
          }}
        >
          Search
        </Button>
      </Box>
    </Flex>
  );

  const showCountries = () => (
    <TableContainer>
      <Table variant="simple" size={"sm"}>
        <Thead>
          <Tr>
            <Th>Code</Th>
            <Th>Name</Th>
            <Th></Th>
          </Tr>
        </Thead>
        <Tbody>
          {pagedRes?.pagedList?.map((item) => (
            <Tr key={item.languageId}>
              <Td>{item.languageCode}</Td>
              <Td>{item.name}</Td>
              <Td>
                <Link
                  mr={2}
                  as={RouteLink}
                  to={"/languages/" + item.languageId + "/edit/"}
                >
                  <UpdateIcon size="xs" fontSize="15" />
                </Link>
                <Link
                  as={RouteLink}
                  to={"/languages/" + item.languageId + "/delete/"}
                >
                  <DeleteIcon size="xs" fontSize="15" />
                </Link>
              </Td>
            </Tr>
          ))}
        </Tbody>
        <Tfoot>
          <Tr>
            <Th colSpan={2} textAlign="center">
              <Button
                isDisabled={!pagedRes?.metaData?.hasPrevious}
                variant="link"
                mr={5}
                onClick={previousPage}
              >
                Previous
              </Button>
              Page {pagedRes?.metaData?.currentPage} of{" "}
              {pagedRes?.metaData?.totalPages}
              <Button
                isDisabled={!pagedRes?.metaData?.hasNext}
                variant="link"
                ml={5}
                onClick={nextPage}
              >
                Next
              </Button>
            </Th>
          </Tr>
        </Tfoot>
      </Table>
    </TableContainer>
  );

  return (
    <Box width={"lg"} p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        {showHeading()}
        {displaySearchBar()}
        {showCountries()}
      </Stack>
    </Box>
  );
}

export default Languages