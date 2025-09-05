import React, { useRef, useEffect } from "react";
import videojs from "video.js";
import "video.js/dist/video-js.css";
import { Box, Typography } from "@mui/material";
import type { VideoInfo } from "../../../services/lessonService";

interface VideoPlayerListProps {
  videoUrls: VideoInfo[];
}

const VideoPlayerList: React.FC<VideoPlayerListProps> = ({ videoUrls }) => {
  if (!videoUrls || videoUrls.length === 0) {
    return <Typography variant="body2">No videos provided.</Typography>;
  }

  return (
    <Box sx={{ margin: "0 auto" }}>
      {videoUrls.map((video, index) => (
        <SingleVideoPlayer
          key={index}
          videoUrl={video.url}
          posterUrl={getPosterFromVideo(video.url)}
          title={video.title}
        />
      ))}
    </Box>
  );
};

// Генерация постера с 1-й секунды
const getPosterFromVideo = (videoUrl: string) => {
  return videoUrl
    .replace("/video/upload/", "/video/upload/so_1/") // Вырезаем кадр на 1-й секунде
    .replace(".mp4", ".jpg");
};

interface SingleVideoPlayerProps {
  videoUrl: string;
  posterUrl: string;
  title: string;
}

const SingleVideoPlayer: React.FC<SingleVideoPlayerProps> = ({
  videoUrl,
  posterUrl,
  title,
}) => {
  const videoRef = useRef<HTMLVideoElement>(null);

  useEffect(() => {
    if (!videoRef.current) return;

    const player = videojs(videoRef.current, {
      controls: true,
      preload: "none", // 👉 Ленивый старт — видео не загружается до нажатия play
      poster: posterUrl, // 👉 Устанавливаем превью
      fluid: true,
      playbackRates: [0.5, 0.75, 1, 1.25, 1.5, 2],
      sources: [{ src: videoUrl, type: "video/mp4" }],
    });

    player.on("play", () =>
      console.log("▶️ Воспроизведение началось:", videoUrl)
    );
    player.on("pause", () =>
      console.log("⏸️ Воспроизведение на паузе:", videoUrl)
    );
    player.on("timeupdate", () =>
      console.log("⏱️ Текущее время:", player.currentTime())
    );
    player.on("ended", () => console.log("✅ Видео закончилось:", videoUrl));

    return () => {
      if (player) {
        player.dispose();
      }
    };
  }, [videoUrl, posterUrl]);

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        mb: 3,
        gap: 2,
        width: "100%",
      }}
    >
      <Typography
        variant="h5"
        sx={{
          color: "#A5B1C2",
          overflowWrap: "break-word",
          wordBreak: "break-word",
          mb: 2,
          maxWidth: "800px",
        }}
        textAlign={"center"}
      >
        {title}
      </Typography>
      <Box
        sx={{
          maxWidth: "800px",
          width: "100%",
          p: 1,
          borderRadius: 3,
          background: `linear-gradient(
                  135deg,
                  #021c2a 0%,
                  #033d5b 25%,
                  #022b3a 50%,
                  #033d5b 75%,
                  #010b11 100%
                )`,
          boxShadow: "0 8px 24px rgba(0, 0, 0, 0.5)",
        }}
      >
        <video ref={videoRef} className="video-js vjs-big-play-centered" />
      </Box>
    </Box>
  );
};

export default VideoPlayerList;
