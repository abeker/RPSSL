import { useEffect, useState } from "react";
import { Container, ThemeProvider } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { fetchChoices, playGame } from "./services/game-service";
import { createPlayer } from "./services/player-service";
import { Choice } from "./models/choice";
import { Result } from "./models/result";
import Header from "./ui-elements/header";
import theme from "./theme";
import "./App.css";
import { getStoredUser, setUser, clearUser } from "./context/userContext";
import RoutesComponent from "./RoutesComponent";
import { AxiosError } from "axios";

function App() {
  const [choices, setChoices] = useState<Choice[] | null>(null);
  const [userChoice, setUserChoice] = useState<Choice | null>(null);
  const [computerChoice, setComputerChoice] = useState<Choice | null>(null);
  const [gameResult, setGameResult] = useState<Result | null>(null);
  const [loading, setLoading] = useState(false);
  const [username, setUsername] = useState<string>("");
  const [isUserLoggedIn, setIsUserLoggedIn] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>("");

  const navigate = useNavigate();

  useEffect(() => {
    const getChoices = async () => {
      const fetchedChoices = await fetchChoices();
      setChoices(fetchedChoices);
    };
    getChoices();
  }, []);

  useEffect(() => {
    const { username, isUserLoggedIn } = getStoredUser();
    setUsername(username);
    setIsUserLoggedIn(isUserLoggedIn);
  }, [isUserLoggedIn]);

  const handleUserChoice = async (choice: Choice) => {
    setUserChoice(choice);
    setLoading(true);
    setGameResult(null);

    const fetchedGameResult = await playGame({"playerName": username, "playerChoiceId": choice.id});
    setGameResult(fetchedGameResult);

    const computerChoiceIndex = fetchedGameResult.computer - 1;
    const computerChoice = choices?.[computerChoiceIndex];
    setComputerChoice(computerChoice || null);
    setLoading(false);
  };

  const handleLogin = async (username: string) => {
    if (username.trim()) {
      try {
        await createPlayer(username);
        setIsUserLoggedIn(true);
        setUser(username);
        resetGameState();
      } catch (error) {
        const axiosError = error as AxiosError;
        if (axiosError.response && axiosError.response.status === 400) {
            setErrorMessage("Player already exists, try with a new name.");
        } else {
            setErrorMessage("An error occurred. Please try again.");
        }
      }
    }
  };

  const handleLogout = () => {
    setUsername("");
    setIsUserLoggedIn(false);
    resetGameState();
    clearUser();
    navigate("/");
  };

  const resetGameState = () => {
    setUserChoice(null);
    setComputerChoice(null);
    setGameResult(null);
    setLoading(false);
    setErrorMessage("");
  };

  return (
    <ThemeProvider theme={theme}>
      <Container
        style={{
          height: "100vh",
          width: '100%',
          display: "flex",
          flexDirection: "column",
          justifyContent: "space-between",
          alignItems: "center",
          padding: "20px",
        }}
      >
        <Header onLogout={handleLogout} isUserLoggedIn={isUserLoggedIn} />
        <RoutesComponent
          isUserLoggedIn={isUserLoggedIn}
          username={username}
          choices={choices}
          userChoice={userChoice}
          computerChoice={computerChoice}
          gameResult={gameResult}
          loading={loading}
          errorMessage={errorMessage}
          onLogin={handleLogin}
          onChoiceSelect={handleUserChoice}
        />
      </Container>
    </ThemeProvider>
  );
}

export default App;
