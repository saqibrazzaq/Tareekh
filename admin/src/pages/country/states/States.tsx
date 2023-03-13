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
  useNavigate,
  useParams,
  useSearchParams,
} from "react-router-dom";
import Common from "../../../utility/Common";
import UpdateIcon from "../../../components/icons/UpdateIcon";
import DeleteIcon from "../../../components/icons/DeleteIcon";
import PagedRes from "../../../dtos/PagedRes";
import { CountryApi } from "../../../api/countryApi";
import { CountryRes } from "../../../dtos/Country";
import { StateRes } from "../../../dtos/State";
import { StateApi } from "../../../api/stateApi";
import TranslationIcon from "../../../components/icons/TranslationIcon";
import CityIcon from "../../../components/icons/CityIcon";

const States = () => {
  const params = useParams();
  const countryId = params.countryId;
  const location = useLocation();
  const navigate = useNavigate();
  const [searchParams, setSearchParams] = useSearchParams(location.search);
  searchParams.set("pageSize", Common.DEFAULT_PAGE_SIZE.toString());
  searchParams.set("countryId", countryId ?? "");

  const [country, setCountry] = useState<CountryRes>();
  const [pagedRes, setPagedRes] = useState<PagedRes<StateRes>>();
  const [searchText, setSearchText] = useState<string>("");

  useEffect(() => {
    searchStates();
    loadCountry();
  }, [searchParams]);

  const loadCountry = () => {
    CountryApi.get(countryId).then(res => {
      setCountry(res);
    })
  }

  const searchStates = () => {
    if (!searchParams) return;
    StateApi.search(Object.fromEntries(searchParams)).then((res) => {
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
        <Heading fontSize={"lg"}>States - {country?.slug}</Heading>
      </Box>
      <Spacer />
      <Box>
        <Link ml={2} as={RouteLink} to={"/countries/" + countryId + "/states/edit"}>
          <Button colorScheme={"blue"} size={"sm"}>
            Add State
          </Button>
          <Button ml={2} size={"sm"} type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
            Back
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

  const showStates = () => (
    <TableContainer>
      <Table variant="simple" size={"sm"}>
        <Thead>
          <Tr>
            <Th>Slug</Th>
            <Th>Names</Th>
            <Th></Th>
          </Tr>
        </Thead>
        <Tbody>
          {pagedRes?.pagedList?.map((item) => (
            <Tr key={item.stateId}>
              <Td>{item.slug}</Td>
              <Td>{item.stateNames?.map(stateName => (
                stateName.name + ", "
              ))}</Td>
              <Td>
              <Link
                  mr={2}
                  as={RouteLink}
                  to={"/countries/" + item.countryId + "/states/" + item.stateId + "/names/"}
                >
                  <TranslationIcon size="xs" fontSize="15" />
                </Link>
                <Link
                  mr={2}
                  as={RouteLink}
                  to={"/countries/" + item.countryId + "/states/" + item.stateId + "/cities/"}
                >
                  <CityIcon size="xs" fontSize="15" />
                </Link>
                <Link
                  mr={2}
                  as={RouteLink}
                  to={"/countries/" + item.countryId + "/states/" + item.stateId + "/edit/"}
                >
                  <UpdateIcon size="xs" fontSize="15" />
                </Link>
                <Link
                  as={RouteLink}
                  to={"/countries/" + item.countryId + "/states/" + item.stateId + "/delete/"}
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
        {showStates()}
      </Stack>
    </Box>
  );
}

export default States