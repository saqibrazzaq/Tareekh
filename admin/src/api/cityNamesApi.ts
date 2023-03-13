import { CityNameReqEdit, CityNameReqSearch } from "../dtos/CityName";
import { api } from "./axiosconfig"

export const CityNamesApi = {
  search: async function (searchParams: CityNameReqSearch) {
    const response = await api.request({
      url: "/cityNames/search",
      method: "GET",
      params: searchParams,
    })

    return response.data
  },
  get: async function (cityNameId?: string) {
    if (!cityNameId) return {};
    const response = await api.request({
      url: `/cityNames/` + cityNameId,
      method: "GET",
    })

    return response.data
  },
  count: async function (stateId?: string) {
    if (!stateId) return {};
    const response = await api.request({
      url: `/cityNames/count/` + stateId,
      method: "GET",
    })

    return response.data
  },
  anyByLanguage: async function (languageId?: string) {
    if (!languageId) return {};
    const response = await api.request({
      url: `/cityNames/anyByLanguage/` + languageId,
      method: "GET",
    })

    return response.data
  },
  create: async function (city: CityNameReqEdit) {
    const response = await api.request({
      url: `/cityNames`,
      method: "POST",
      data: city,
    })

    return response.data
  },
  update: async function (cityNameId?: string, city?: CityNameReqEdit) {
    await api.request({
      url: `/cityNames/` + cityNameId,
      method: "PUT",
      data: city,
    })
  },
  delete: async function (cityNameId?: string) {
    const response = await api.request({
      url: `/cityNames/` + cityNameId,
      method: "DELETE",
    })

    return response.data
  },
}