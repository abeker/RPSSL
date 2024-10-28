import {baseAxios} from '../axios/api-config';
import { Scoreboard } from '../models/scoreboard';

export async function createPlayer(playerName: string): Promise<void> {
    const response = await baseAxios.post('/players', { name: playerName });
    return response.data;
}

export async function getPlayerByName(playerName: string): Promise<void> {
    const response = await baseAxios.get(`/players/name/${playerName}`);
    return response.data;
}

export async function getScoreboard(index: number, size: number): Promise<Scoreboard> {
    const response = await baseAxios.get('/players/scoreboard', {
        params: {
            Index: index,
            Size: size
        }
    });
    return response.data;
}
