import { useEffect, useState } from 'react';
import { Box, Container } from '@mui/material';
import { fetchChoices, fetchComputerChoice, playGame } from './services/game-service';
import { Choice } from './models/choice';
import { Result } from './models/result';
import ChoiceSelector from './ui-elements/choice-selector';
import Header from './ui-elements/header';
import UserChoice from './ui-elements/user-choice';
import ComputerChoice from './ui-elements/computer-choice';
import GameResult from './ui-elements/game-result';
import './App.css';

function App() {
    const [choices, setChoices] = useState<Choice[] | null>(null);
    const [userChoice, setUserChoice] = useState<Choice | null>(null);
    const [computerChoice, setComputerChoice] = useState<Choice | null>(null);
    const [gameResult, setGameResult] = useState<Result | null>(null);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        const getChoices = async () => {
            const fetchedChoices = await fetchChoices();
            setChoices(fetchedChoices);
        };

        getChoices();
    }, []);

    const handleUserChoice = async (choice: Choice) => {
        setUserChoice(choice);
        setLoading(true);
        setGameResult(null);

        const fetchedComputerChoice = await fetchComputerChoice();
        setComputerChoice(fetchedComputerChoice);

        const fetchedGameResult = await playGame(choice.id);
        setGameResult(fetchedGameResult);

        const computerChoiceIndex = fetchedGameResult.computer - 1;
        const computerChoice = choices?.[computerChoiceIndex];
        setComputerChoice(computerChoice || null);
        setLoading(false);
    };

    return (
        <Container
            style={{
                height: '100vh',
                width: '100%',
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'space-between',
                alignItems: 'center',
                padding: '20px',
            }}
        >
            <Header />
            <Box style={{ display: 'flex', justifyContent: 'center', width: '100%' }}>
                <UserChoice userChoice={userChoice} />
                <ComputerChoice computerChoice={computerChoice} loading={loading} />
            </Box>
            <GameResult result={gameResult?.results || null} />
            <ChoiceSelector choices={choices} onChoiceSelect={handleUserChoice} />
        </Container>
    );
}

export default App;
