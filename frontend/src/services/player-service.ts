import {baseAxios} from '../axios/api-config';

export async function createPlayer(playerName: string): Promise<void> {
    const response = await baseAxios.post('/players', { name: playerName });
    return response.data;
}
