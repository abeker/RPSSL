import { Box, Typography, CircularProgress, Link } from "@mui/material";
import { useEffect, useState } from "react";
import { useTheme } from "@mui/material/styles";
import { getScoreboard } from "../services/player-service";
import { Scoreboard as ScoreboardType } from "../models/scoreboard";
import { Link as RouterLink } from "react-router-dom";

interface ScoreboardProps {
  index: number;
  size: number;
}

const Scoreboard = ({ index, size }: ScoreboardProps) => {
  const [playerNames, setPlayerNames] = useState<string[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const theme = useTheme();

  useEffect(() => {
    const fetchScoreboard = async () => {
      setLoading(true);

      const response: ScoreboardType = await getScoreboard(index, size);
      console.log(response);
      setPlayerNames(response.playerNames);
      setLoading(false);
    };

    fetchScoreboard();
  }, [index, size]);

  if (loading) {
    return <CircularProgress />;
  }

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
      <Typography variant="h4" style={{ color: theme.palette.primary.main }}>
        Best players
      </Typography>
      <Box>
        {playerNames.map((name, idx) => (
          <Typography
            key={idx}
            variant="h6"
            style={{ color: theme.palette.primary.main }}
          >
            {name}
          </Typography>
        ))}
      </Box>
      <Link
        component={RouterLink}
        to="/"
        variant="body1"
        style={{
          marginTop: '30px',
          display: 'block',
          color: theme.palette.primary.main,
          textDecoration: 'underline'
        }}
      >
        Play game
      </Link>
    </Box>
  );
};

export default Scoreboard;
