import { Select } from 'chakra-react-select';
import React, { useEffect, useState } from 'react'
import { TimezoneApi } from '../api/timezoneApi';
import { TimezoneReqSearch, TimezoneRes } from '../dtos/Timezone';

interface TimezoneDropdownParams {
  handleChange?: any;
  selectedTimezone?: TimezoneRes;
}

const TimezoneDropdown = ({handleChange, selectedTimezone}: TimezoneDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<TimezoneRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadTimezones = () => {
    setIsLoading(true);
    TimezoneApi.search(new TimezoneReqSearch({ searchText: inputValue }, {}))
      .then((res) => {
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadTimezones();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
        size={"sm"}
        getOptionLabel={(c) => (c.cityName + " - " + c.gmtOffsetName) || ""}
        getOptionValue={(c) => c.timezoneId || ""}
        options={items}
        onChange={handleChange}
        onInputChange={handleInputChange}
        isClearable={true}
        placeholder="Select Time Zone..."
        isLoading={isLoading}
        value={selectedTimezone}
      ></Select>
  );
}

export default TimezoneDropdown