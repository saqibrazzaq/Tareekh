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
import Common from "../../../../../utility/Common";
import UpdateIcon from "../../../../../components/icons/UpdateIcon";
import DeleteIcon from "../../../../../components/icons/DeleteIcon";
import PagedRes from "../../../../../dtos/PagedRes";
import { CityRes } from "../../../../../dtos/City";
import { CityNameRes } from "../../../../../dtos/CityName";
import { CityApi } from "../../../../../api/cityApi";
import { CityNamesApi } from "../../../../../api/cityNamesApi";

const CityNames = () => {
  const params = useParams();
  const cityId = params.cityId;
  const countryId = params.countryId;
  const stateId = params.stateId;
  const location = useLocation();
  const navigate = useNavigate();
  const [searchParams, setSearchParams] = useSearchParams(location.search);
  searchParams.set("pageSize", Common.DEFAULT_PAGE_SIZE.toString());
  searchParams.set("cityId", cityId ?? "");

  const [city, setCity] = useState<CityRes>();
  const [pagedRes, setPagedRes] = useState<PagedRes<CityNameRes>>();
  const [searchText, setSearchText] = useState<string>("");

  useEffect(() => {
    searchCityNames();
    loadCity();
  }, [searchParams]);

  const loadCity = () => {
    CityApi.get(cityId).then(res => {
      setCity(res);
    })
  }

  const searchCityNames = () => {
    if (!searchParams) return;
    CityNamesApi.search(Object.fromEntries(searchParams)).then((res) => {
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
        <Heading fontSize={"lg"}>
          Names - {city?.slug + ", " + city?.state?.slug + ", " + city?.state?.country?.slug}
        </Heading>
      </Box>
      <Spacer />
      <Box>
        <Link ml={2} as={RouteLink} 
          to={"/countries/" + countryId
            + "/states/" + stateId + "/cities/" + cityId + "/names/edit"}>
          <Button colorScheme={"blue"} size={"sm"}>
            Add Name
          </Button>
        </Link>
        <Button ml={2} size={"sm"} type="button" colorScheme={"gray"} onClick={() => navigate(-1)}>
          Back
        </Button>
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

  const showCityNames = () => (
    <TableContainer>
      <Table variant="simple" size={"sm"}>
        <Thead>
          <Tr>
            <Th>Name</Th>
            <Th>Language</Th>
            <Th></Th>
          </Tr>
        </Thead>
        <Tbody>
          {pagedRes?.pagedList?.map((item) => (
            <Tr key={item.cityNameId}>
              <Td>{item.name}</Td>
              <Td>{item.language?.name}</Td>
              <Td>
                <Link
                  mr={2}
                  as={RouteLink}
                  to={"/countries/" + countryId
                  + "/states/" + stateId + "/cities/" + cityId + "/names/" + item.cityNameId + "/edit/"}
                >
                  <UpdateIcon size="xs" fontSize="15" />
                </Link>
                <Link
                  as={RouteLink}
                  to={"/countries/" + countryId
                  + "/states/" + stateId + "/cities/" + cityId + "/names/" + item.cityNameId + "/delete/"}
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
    <Box width={"2xl"} p={4}>
      <Stack spacing={4} as={Container} maxW={"3xl"}>
        {showHeading()}
        {displaySearchBar()}
        {showCityNames()}
      </Stack>
    </Box>
  );
}

export default CityNames