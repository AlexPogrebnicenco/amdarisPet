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
import ChangeHistoryOutlinedIcon from "@mui/icons-material/ChangeHistoryOutlined";
import PlayCircleOutlineIcon from "@mui/icons-material/PlayCircleOutline";
import PersonIcon from "@mui/icons-material/Person";
import { courseCategories } from "../../../constants/courseCategories";
import { difficultyLevels } from "../../../constants/difficultyLevels";

interface BasicCardProps {
  title: string;
  description: string;
  modules: number;
  difficulty: "Beginner" | "Intermediate" | "Advanced";
  duration?: string;
  category?: string;
  isNew?: boolean;
  isPro?: boolean;
  avatars?: string[]; // array of image URLs
  onClick?: () => void;
}

const BasicCard: React.FC<BasicCardProps> = ({
  title,
  description,
  modules,
  difficulty,
  duration,
  category,
  isNew = false,
  isPro = false,
  avatars,
  onClick,
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
    <Box
      sx={{
        position: "relative",
        cursor: onClick ? "pointer" : "default",
      }}
      onClick={onClick}
    >
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
          transition: "background-color 0.3s ease", // плавный переход
          "&:hover": {
            backgroundColor: onClick
              ? theme.palette.card.hoverBackground
              : theme.palette.card.hoverBackground,
          },
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
            alignItems: "flex-end",
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
            {/* Category */}
            {(() => {
              const matched = courseCategories.find((c) => c.name === category);

              return (
                <Box sx={{ display: "flex", alignItems: "flex-end", gap: 0.5 }}>
                  {matched && <>{matched.icon}</>}
                  <Typography
                    variant="body2"
                    sx={{ color: "#A5B1C2", fontSize: 11, lineHeight: 1 }}
                  >
                    {category}
                  </Typography>
                </Box>
              );
            })()}

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
            {(() => {
              const matched = difficultyLevels.find(
                (d) => d.level === difficulty
              );
              return (
                <Box sx={{ display: "flex", alignItems: "flex-end", gap: 0.5 }}>
                  {matched?.icon}
                  <Typography
                    variant="body2"
                    sx={{ color: "#A5B1C2", fontSize: 11, lineHeight: 1 }}
                  >
                    {difficulty}
                  </Typography>
                </Box>
              );
            })()}
          </Box>

          {/* Аватары */}

          <AvatarGroup max={isTight ? 1 : 4} spacing="small">
            {avatars?.length ? (
              avatars.map((src, index) => (
                <Tooltip key={index} title={`Contributor ${index + 1}`}>
                  <Avatar
                    alt={`avatar-${index}`}
                    src={src ?? undefined}
                    sx={{
                      width: 48,
                      height: 48,
                      border: "2px solid #14151A",
                    }}
                  >
                    {!src && <PersonIcon sx={{ fontSize: 20 }} />}
                  </Avatar>
                </Tooltip>
              ))
            ) : (
              <Avatar
                sx={{
                  width: 48,
                  height: 48,
                  border: "2px solid #14151A",
                }}
              >
                <PersonIcon sx={{ fontSize: 20 }} />
              </Avatar>
            )}
          </AvatarGroup>
        </Box>
      </Box>
    </Box>
  );
};

export default BasicCard;
