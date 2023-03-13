import { StateNameReqEdit, StateNameReqSearch } from "../dtos/StateName";
import { api } from "./axiosconfig"

export const StateNamesApi = {
  search: async function (searchParams: StateNameReqSearch) {
    const response = await api.request({
      url: "/stateNames/search",
      method: "GET",
      params: searchParams,
    })

    return response.data
  },
  get: async function (stateNameId?: string) {
    if (!stateNameId) return {};
    const response = await api.request({
      url: `/stateNames/` + stateNameId,
      method: "GET",
    })

    return response.data
  },
  count: async function (stateId?: string) {
    if (!stateId) return {};
    const response = await api.request({
      url: `/stateNames/count/` + stateId,
      method: "GET",
    })

    return response.data
  },
  anyByLanguage: async function (languageId?: string) {
    if (!languageId) return {};
    const response = await api.request({
      url: `/stateNames/anyByLanguage/` + languageId,
      method: "GET",
    })

    return response.data
  },
  create: async function (city: StateNameReqEdit) {
    const response = await api.request({
      url: `/stateNames`,
      method: "POST",
      data: city,
    })

    return response.data
  },
  update: async function (stateNameId?: string, state?: StateNameReqEdit) {
    await api.request({
      url: `/stateNames/` + stateNameId,
      method: "PUT",
      data: state,
    })
  },
  delete: async function (stateNameId?: string) {
    const response = await api.request({
      url: `/stateNames/` + stateNameId,
      method: "DELETE",
    })

    return response.data
  },
}