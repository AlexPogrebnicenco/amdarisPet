import React, { useRef, useState, useEffect } from "react";
import {
  Box,
  Typography,
  Chip,
  Avatar,
  Tooltip,
  useTheme,
  AvatarGroup,
} from "@mui/material";
import HourglassEmptyIcon from "@mui/icons-material/HourglassEmpty";
import ChangeHistoryOutlinedIcon from "@mui/icons-material/ChangeHistoryOutlined";
import MenuBookRoundedIcon from "@mui/icons-material/MenuBookRounded";
import PlayCircleOutlineIcon from "@mui/icons-material/PlayCircleOutline";

interface BasicCardProps {
  title: string;
  description: string;
  modules: number;
  difficulty: "Beginner" | "Intermediate" | "Advanced";
  duration: string;
  tag?: string;
  isNew?: boolean;
  isPro?: boolean;
  avatars: string[]; // array of image URLs
}

const BasicCard: React.FC<BasicCardProps> = ({
  title,
  description,
  modules,
  difficulty,
  duration,
  tag = "Fullstack",
  isNew = false,
  isPro = false,
  avatars,
}) => {
  const theme = useTheme();
  const cardRef = useRef<HTMLDivElement>(null);
  const [isCompact, setIsCompact] = useState(false);
  const [isTight, setIsTight] = useState(false);

  useEffect(() => {
    const observer = new ResizeObserver((entries) => {
      for (let entry of entries) {
        const width = entry.contentRect.width;
        setIsCompact(width <= 410);
        setIsTight(width <= 492);
      }
    });

    if (cardRef.current) {
      observer.observe(cardRef.current);
    }

    return () => {
      if (cardRef.current) {
        observer.unobserve(cardRef.current);
      }
    };
  }, []);

  const displayedDescription =
    isCompact && description.length > 100
      ? description.slice(0, 100) + "..."
      : description;

  return (
    <Box sx={{ position: "relative" }}>
      {/* BADGES вне карточки, но внутри обёртки */}
      <Box
        sx={{
          position: "absolute",
          top: 8,
          right: 8,
          display: "flex",
          gap: 1,
          zIndex: 1,
        }}
      >
        {isPro && (
          <Chip
            size="small"
            label="PRO"
            sx={{
              backgroundColor: theme.palette.card.badgeProBg,
              color: theme.palette.card.badgeText,
              fontSize: 10,
              height: 16,
              pt: 0.25,
              "& .MuiChip-label": {
                padding: 0.75,
              },
            }}
          />
        )}
        {isNew && (
          <Chip
            size="small"
            label="NEW"
            sx={{
              backgroundColor: theme.palette.card.badgeNewBg,
              color: theme.palette.card.badgeText,
              fontSize: 10,
              height: 16,
              pt: 0.25,
              px: 0.25,
              "& .MuiChip-label": {
                padding: 0.75,
              },
            }}
          />
        )}
      </Box>

      {/* Основной CARD */}
      <Box
        ref={cardRef}
        sx={{
          backgroundColor: theme.palette.card.background,
          borderRadius: 2,
          p: 3,
          display: "flex",
          flexDirection: "column",
          justifyContent: "space-between",
          height: "100%",
          minHeight: 250,
          maxHeight: 250,
          overflow: "hidden",
        }}
      >
        {/* ...весь остальной контент карточки — title, description, meta, avatars */}
        {/* Title */}
        <Typography
          variant="h6"
          fontWeight={600}
          gutterBottom
          sx={{ color: theme.palette.card.text }}
        >
          {title}
        </Typography>

        {/* Description */}
        <Typography
          variant="body2"
          sx={{
            flexGrow: 1,
            mb: 2,
            color: theme.palette.card.mutedText,
          }}
        >
          {displayedDescription}
        </Typography>
        {/*////////////////////////////////////////////////////////////////// */}

        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "flex-end", // прижимает левый блок вниз, к аватарам
            width: "100%",
          }}
        >
          {/* Мета-информация */}
          <Box
            sx={{
              display: isTight ? "grid" : "flex",
              gridTemplateColumns: isTight ? "repeat(2, 1fr)" : "none",
              gap: 1,
              flexWrap: "wrap",
              alignItems: "flex-end",
            }}
          >
            {/* Tag */}
            <Box sx={{ display: "flex", alignItems: "flex-end", gap: 0.5 }}>
              <MenuBookRoundedIcon sx={{ fontSize: 12, color: "#4caf50" }} />
              <Typography
                variant="body2"
                sx={{ color: "#A5B1C2", fontSize: 11, lineHeight: 1 }}
              >
                {tag}
              </Typography>
            </Box>

            {/* Lessons */}
            <Box sx={{ display: "flex", alignItems: "flex-end", gap: 0.5 }}>
              <PlayCircleOutlineIcon sx={{ fontSize: 12, color: "#FBC02D" }} />
              <Typography
                variant="body2"
                sx={{ color: "#A5B1C2", fontSize: 11, lineHeight: 1 }}
              >
                {modules} lessons
              </Typography>
            </Box>

            {/* Difficulty */}
            <Box sx={{ display: "flex", alignItems: "flex-end", gap: 0.5 }}>
              <ChangeHistoryOutlinedIcon
                sx={{ fontSize: 12, color: "#A2A226" }}
              />
              <Typography
                variant="body2"
                sx={{ color: "#A5B1C2", fontSize: 11, lineHeight: 1 }}
              >
                {difficulty}
              </Typography>
            </Box>

            {/* Duration */}
            <Box sx={{ display: "flex", alignItems: "flex-end", gap: 0.5 }}>
              <HourglassEmptyIcon sx={{ fontSize: 12, color: "#90CAF9" }} />
              <Typography
                variant="body2"
                sx={{ color: "#A5B1C2", fontSize: 11, lineHeight: 1 }}
              >
                {duration}
              </Typography>
            </Box>
          </Box>

          {/* Аватары */}

          <AvatarGroup max={isTight ? 1 : 4} spacing="small">
            {avatars.map((src, index) => (
              <Tooltip key={index} title={`Contributor ${index + 1}`}>
                <Avatar
                  alt={`avatar-${index}`}
                  src={src}
                  sx={{
                    width: 48,
                    height: 48,
                    border: "2px solid #14151A",
                  }}
                />
              </Tooltip>
            ))}
          </AvatarGroup>
        </Box>
      </Box>
    </Box>
  );
};

export default BasicCard;
