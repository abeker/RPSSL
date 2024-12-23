import axios from 'axios';

export const createAxiosInstance = (baseURL: string) => {
    return axios.create({
        baseURL,
        headers: {
            'Content-Type': 'application/json',
        },
    });
};

const API_BASE_URL = import.meta.env.VITE_RPSSL_API_BASE_URL + '/api/v1.0';
const TEST_BASE_URL = import.meta.env.VITE_RPSSL_API_BASE_URL;


export const baseAxios = createAxiosInstance(API_BASE_URL);
export const testAxios = createAxiosInstance(TEST_BASE_URL);
