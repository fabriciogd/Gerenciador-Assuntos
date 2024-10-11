import axios from "@/plugins/axios";

export async function fetch() {
    return axios.get(`/dashboard`)
}