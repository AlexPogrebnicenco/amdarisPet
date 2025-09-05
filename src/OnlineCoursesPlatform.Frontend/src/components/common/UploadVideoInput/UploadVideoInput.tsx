import { useState, type ChangeEvent } from "react";
import {
  LinearProgress,
  Typography,
  Box,
  Button,
  Tooltip,
  IconButton,
} from "@mui/material";
import CloudUploadIcon from "@mui/icons-material/CloudUpload";
import { getCloudinarySignature } from "../../../services/cloudinaryService";
import { type UseFormSetValue, type UseFormWatch } from "react-hook-form";
import type { LessonFormInputs } from "../../../pages/Teacher/AddLesson/AddLessonForm";
import { toast } from "react-toastify";
import CancelTwoToneIcon from "@mui/icons-material/CancelTwoTone";
import CustomTextField from "../CustomTextField/CustomTextField";
import HelpTwoToneIcon from "@mui/icons-material/HelpTwoTone";

interface UploadVideoInputProps {
  name: `videoUrls.${number}`;
  setValue: UseFormSetValue<LessonFormInputs>;
  watch: UseFormWatch<LessonFormInputs>;
  onRemove: () => void;
}

const UploadVideoInput: React.FC<UploadVideoInputProps> = ({
  name,
  setValue,
  watch,
  onRemove,
}) => {
  const [uploadProgress, setUploadProgress] = useState(0);
  const [uploading, setUploading] = useState(false);
  const [customFileName, setCustomFileName] = useState("");
  const [originalFileName, setOriginalFileName] = useState("");

  const uploadedUrl = watch(`${name}.url`);

  const handleFileChange = async (e: ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files[0]) {
      try {
        const file = e.target.files[0];
        setUploading(true);
        setOriginalFileName(file.name); // сохраняем оригинальное имя файла

        const signatureData = await getCloudinarySignature(customFileName);

        const url = `https://api.cloudinary.com/v1_1/${signatureData.cloudName}/video/upload`;
        const formData = new FormData();
        formData.append("file", file);
        formData.append("api_key", signatureData.apiKey);
        formData.append("timestamp", signatureData.timestamp.toString());
        formData.append("signature", signatureData.signature);

        if (customFileName.trim()) {
          formData.append("context", `caption=${customFileName.trim()}`);
        }

        const xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", (event) => {
          if (event.lengthComputable) {
            const progress = Math.round((event.loaded * 100) / event.total);
            setUploadProgress(progress);
          }
        });

        xhr.onreadystatechange = () => {
          if (xhr.readyState === 4) {
            setUploading(false);
            if (xhr.status === 200) {
              const response = JSON.parse(xhr.responseText);
              setValue(`${name}.url`, response.secure_url, {
                shouldValidate: true,
              });
              setValue(`${name}.title`, customFileName.trim() || file.name);
              toast.success("Video uploaded successfully");
            } else {
              toast.error("Video upload failed");
            }
          }
        };

        xhr.open("POST", url, true);
        xhr.send(formData);
      } catch (error) {
        console.error("Video upload failed", error);
        toast.error("Video upload failed");
        setUploading(false);
      }
    }
  };

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "row",
        gap: 1,
        alignItems: "center",
        width: "100%",
      }}
    >
      {uploadedUrl ? (
        <Box sx={{ display: "flex", flexDirection: "column", maxWidth: "900px" }}>
          {originalFileName && (
            <Tooltip title={originalFileName}>
              <Typography
                variant="body2"
                sx={{
                  color: "#A5B1C2",
                  overflowWrap: "break-word",
                  wordBreak: "break-word",
                }}
              >
                Original: {originalFileName}
              </Typography>
            </Tooltip>
          )}
          {customFileName.trim() && (
            <Tooltip title={customFileName}>
              <Typography
                variant="body2"
                sx={{
                  color: "#6EC76E",
                  overflowWrap: "break-word",
                  wordBreak: "break-word",
                }}
              >
                Named: {customFileName}
              </Typography>
            </Tooltip>
          )}
        </Box>
      ) : (
        !uploading && (
          <Box
            sx={{
              display: "flex",
              alignItems: "center",
              flexDirection: "column",
              gap: 1,
              py: 1,
              width: "100%",
            }}
          >
            <CustomTextField
              label="Video Name"
              size="small"
              variant="outlined"
              value={customFileName}
              onChange={(e) => setCustomFileName(e.target.value)}
            />

            <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
              <Button
                component="label"
                variant="contained"
                color="secondary"
                startIcon={<CloudUploadIcon />}
                sx={{
                  backgroundColor: "#A5B1C2",
                  "&:hover": {
                    backgroundColor: "#00B300",
                  },
                  whiteSpace: "nowrap",
                  overflow: "hidden",
                  textOverflow: "ellipsis",
                  px: 3,
                  py: 0.5,
                  minWidth: 150,
                }}
              >
                Upload Video
                <input
                  type="file"
                  accept="video/*"
                  onChange={handleFileChange}
                  hidden
                />
              </Button>

              <IconButton
                onClick={(e) => {
                  e.stopPropagation();
                  onRemove();
                }}
                sx={{
                  transition: "background-color 0.3s, color 0.3s",
                  "&:hover": {
                    backgroundColor: "#38424D",
                    "& svg": { color: "#FF4C4C" },
                  },
                }}
              >
                <CancelTwoToneIcon sx={{ color: "#A5B1C2" }} />
              </IconButton>

              <Tooltip
                children={<HelpTwoToneIcon sx={{ color: "#A5B1C2", mr: 1 }} />}
                title={"No video selected. Adding a video is optional."}
              />
            </Box>
          </Box>
        )
      )}

      {uploading && (
        <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
          <LinearProgress
            variant="determinate"
            value={uploadProgress}
            sx={{
              flex: 1,
              height: 10,
              minWidth: 100,
              backgroundColor: "#A5B1C2",
              "& .MuiLinearProgress-bar": {
                backgroundColor: "#00B300",
              },
            }}
          />
          <Typography variant="body2" align="center" sx={{ minWidth: 30 }}>
            {uploadProgress}%
          </Typography>
        </Box>
      )}
    </Box>
  );
};

export default UploadVideoInput;
