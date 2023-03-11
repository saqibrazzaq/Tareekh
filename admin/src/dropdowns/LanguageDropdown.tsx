import { Select } from 'chakra-react-select';
import React, { useEffect, useState } from 'react'
import { LanguageApi } from '../api/languageApi';
import { LanguageReqSearch, LanguageRes } from '../dtos/Language';

interface LanguageDropdownParams {
  handleChange?: any;
  selectedLanguage?: LanguageRes;
}

const LanguageDropdown = ({handleChange, selectedLanguage}: LanguageDropdownParams) => {
  const [inputValue, setInputValue] = useState("");
  const [items, setItems] = useState<LanguageRes[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadLanguages = () => {
    setIsLoading(true);
    LanguageApi.search(new LanguageReqSearch({ searchText: inputValue }, {}))
      .then((res) => {
        setItems(res.pagedList);
      })
      .finally(() => setIsLoading(false));
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      loadLanguages();
    }, 1000);

    return () => clearTimeout(timer);
  }, [inputValue]);

  const handleInputChange = (newValue: string) => {
    setInputValue(newValue);
  };

  return (
    <Select
        size={"sm"}
        getOptionLabel={(c) => c.name || ""}
        getOptionValue={(c) => c.languageId || ""}
        options={items}
        onChange={handleChange}
        onInputChange={handleInputChange}
        isClearable={true}
        placeholder="Select language..."
        isLoading={isLoading}
        value={selectedLanguage}
      ></Select>
  );
}

export default LanguageDropdown