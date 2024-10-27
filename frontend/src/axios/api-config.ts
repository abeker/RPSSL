import axios from 'axios';

export const createAxiosInstance = (baseURL: string) => {
    return axios.create({
        baseURL,
        headers: {
            'Content-Type': 'application/json',
        },
    });
};

const API_BASE_URL = 'https://codechallenge.boohma.com';

export const baseAxios = createAxiosInstance(API_BASE_URL);
