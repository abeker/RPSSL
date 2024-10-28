import {baseAxios, testAxios} from '../axios/api-config';
import { Choice } from '../models/choice';
import { GameRound } from '../models/gameRound';
import { Result } from '../models/result';

export async function fetchChoices(): Promise<Choice[]> {
    const response = await baseAxios.get('/choices');
    return response.data;
}

export async function fetchComputerChoice(): Promise<Choice> {
    const response = await baseAxios.get('/choice');
    return response.data;
}

export async function playTestGame(choiceId: number): Promise<Result> {
    const response = await testAxios.post('/play', { player: choiceId });
    return response.data;
}

export async function playGame(gameRound: GameRound): Promise<Result> {
    const response = await baseAxios.post('/games', gameRound);
    return response.data;
}
