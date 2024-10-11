import axios from "@/plugins/axios";

export async function fetch() {
    return axios.get(`/assuntos`)
}

export async function getById(id) {
    return axios.get(`/assuntos/${id}`)
}

export async function insert(data) {
    return axios.post(`/assuntos`, data)
}

export async function update(id, data) {
    return axios.put(`/assuntos/${id}`, data)
}

export async function remove(id) {
    return axios.delete(`/assuntos/${id}`)
}

export async function search(id) {
    return axios.post(`/assuntos/search`, { id })
}

export async function fetchLinks(id) {
    return axios.get(`/assuntos/${id}/links`)
}