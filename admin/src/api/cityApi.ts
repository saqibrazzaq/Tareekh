import { CityReqEdit, CityReqSearch } from "../dtos/City";
import { api } from "./axiosconfig"

export const CityApi = {
  search: async function (searchParams: CityReqSearch) {
    const response = await api.request({
      url: "/cities/search",
      method: "GET",
      params: searchParams,
    })

    return response.data
  },
  get: async function (cityId?: string) {
    if (!cityId) return {};
    const response = await api.request({
      url: `/cities/` + cityId,
      method: "GET",
    })

    return response.data
  },
  findBySlug: async function (slug?: string) {
    if (!slug) return {};
    const response = await api.request({
      url: `/cities/slug/` + slug,
      method: "GET",
    })

    return response.data
  },
  count: async function () {
    const response = await api.request({
      url: `/cities/count`,
      method: "GET",
    })

    return response.data
  },
  countByState: async function (stateId?: string) {
    if (!stateId) return {};
    const response = await api.request({
      url: `/cities/count/` + stateId,
      method: "GET",
    })

    return response.data
  },
  anyByTimezone: async function (timezoneId?: string) {
    if (!timezoneId) return {};
    const response = await api.request({
      url: `/cities/anyByTimezone/` + timezoneId,
      method: "GET",
    })

    return response.data
  },
  create: async function (city: CityReqEdit) {
    const response = await api.request({
      url: `/cities`,
      method: "POST",
      data: city,
    })

    return response.data
  },
  update: async function (cityId?: string, city?: CityReqEdit) {
    await api.request({
      url: `/cities/` + cityId,
      method: "PUT",
      data: city,
    })
  },
  delete: async function (cityId?: string) {
    const response = await api.request({
      url: `/cities/` + cityId,
      method: "DELETE",
    })

    return response.data
  },
}