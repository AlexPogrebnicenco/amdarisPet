import React, { useState, useRef, useEffect } from "react";
import { Typography, Box, Button } from "@mui/material";

interface ExpandableTextLessonProps {
  text: string;
  maxLines?: number;
}

const ExpandableTextLesson: React.FC<ExpandableTextLessonProps> = ({
  text,
  maxLines = 3,
}) => {
  const [expanded, setExpanded] = useState(false);
  const [isTruncated, setIsTruncated] = useState(false);
  const textRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (textRef.current && !expanded) {
      const hasOverflow =
        textRef.current.scrollHeight > textRef.current.clientHeight;
      setIsTruncated(hasOverflow);
    }
  }, [text, expanded]);

  return (
    <Box sx={{ position: "relative"}}>
      <Typography
        variant="body1"
        ref={textRef}
        sx={{
          display: "-webkit-box",
          WebkitLineClamp: expanded ? "unset" : maxLines,
          WebkitBoxOrient: "vertical",
          overflow: "hidden",
          textOverflow: "ellipsis",
          transition: "max-height 0.3s ease",
        }}
      >
        {text}
      </Typography>

      {/* Градиент + кнопка */}
      {!expanded && isTruncated && (
        <Box
          sx={{
            position: "absolute",
            bottom: 0,
            right: 0,
            width: "100%",
            height: "3em",
            background: "linear-gradient(to top, #1C252E, rgba(22, 26, 29, 0))",
            display: "flex",
            justifyContent: "flex-end",
            alignItems: "flex-end",
          }}
        >
          {/* Контейнер для градиента и кнопки */}
          <Box sx={{ display: "flex", alignItems: "stretch", }}>
            {/* Градиент слева от кнопки */}
            <Box
              sx={{
                width: "200px",
                background:
                  "linear-gradient(to right, rgba(22, 26, 29, 0), #1C252E)",
              }}
            />

            {/* Кнопка */}
            <Button
              onClick={() => setExpanded(true)}
              size="small"
              sx={{
                color: "#0369A1",
                fontSize: "0.8rem",
                minWidth: "auto",
                p: 0,
                backgroundColor: "#1C252E",
              }}
            >
              Show More ▼
            </Button>
            <Box
              sx={{
                width: "20px",
                background: "#1C252E",
              }}
            />
          </Box>
        </Box>
      )}

      {expanded && (
        <Box sx={{ mt: 1 }}>
          <Button
            onClick={() => setExpanded(false)}
            size="small"
            sx={{
              color: "#0369A1",
              fontSize: "0.8rem",
              minWidth: "auto",
              padding: 0,
              backgroundColor: "#1C252E",
            }}
          >
            Show Less ▲
          </Button>
        </Box>
      )}
    </Box>
  );
};

export default ExpandableTextLesson;
