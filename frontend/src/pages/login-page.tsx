import { Box, TextField, Button } from "@mui/material";
import { useState } from "react";

interface LoginPageProps {
  onLogin: (username: string) => void;
  errorMessage: string;
}

function LoginPage({ onLogin, errorMessage }: LoginPageProps) {
  const [username, setUsername] = useState<string>("");

  const handleLogin = () => {
    onLogin(username);
  };

  return (
    <Box
      style={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        height: "100%",
      }}
    >
      <TextField
        placeholder="Enter your username"
        variant="outlined"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
        error={!!errorMessage}
        helperText={errorMessage}
      />
      <Button variant="contained" color="primary" onClick={handleLogin}>
        Start game
      </Button>
    </Box>
  );
}

export default LoginPage;
