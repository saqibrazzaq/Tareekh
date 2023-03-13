import { CountryNameReqEdit, CountryNameReqSearch } from "../dtos/CountryName";
import { api } from "./axiosconfig"

export const CountryNamesApi = {
  search: async function (searchParams: CountryNameReqSearch) {
    const response = await api.request({
      url: "/countryNames/search",
      method: "GET",
      params: searchParams,
    })

    return response.data
  },
  get: async function (countryNameId?: string) {
    if (!countryNameId) return {};
    const response = await api.request({
      url: `/countryNames/` + countryNameId,
      method: "GET",
    })

    return response.data
  },
  count: async function (countryId?: string) {
    if (!countryId) return {};
    const response = await api.request({
      url: `/countryNames/count/` + countryId,
      method: "GET",
    })

    return response.data
  },
  anyByLanguage: async function (languageId?: string) {
    if (!languageId) return {};
    const response = await api.request({
      url: `/countryNames/anyByLanguage/` + languageId,
      method: "GET",
    })

    return response.data
  },
  create: async function (country: CountryNameReqEdit) {
    const response = await api.request({
      url: `/countryNames`,
      method: "POST",
      data: country,
    })

    return response.data
  },
  update: async function (countryNameId?: string, country?: CountryNameReqEdit) {
    await api.request({
      url: `/countryNames/` + countryNameId,
      method: "PUT",
      data: country,
    })
  },
  delete: async function (countryNameId?: string) {
    const response = await api.request({
      url: `/countryNames/` + countryNameId,
      method: "DELETE",
    })

    return response.data
  },
}