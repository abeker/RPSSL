import {baseAxios, testAxios} from '../axios/api-config';
import { Choice } from '../models/choice';
import { Result } from '../models/result';

export async function fetchChoices(): Promise<Choice[]> {
    const response = await baseAxios.get('/choices');
    return response.data;
}

export async function fetchComputerChoice(): Promise<Choice> {
    const response = await baseAxios.get('/choice');
    return response.data;
}

export async function playGame(choiceId: number): Promise<Result> {
    const response = await testAxios.post('/play', { player: choiceId });
    return response.data;
}
