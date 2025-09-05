import { useState, type ChangeEvent } from "react";
import {
  LinearProgress,
  Typography,
  Box,
  Button,
  Tooltip,
  IconButton,
  useTheme,
} from "@mui/material";
import CloudUploadIcon from "@mui/icons-material/CloudUpload";
import CancelTwoToneIcon from "@mui/icons-material/CancelTwoTone";
import HelpTwoToneIcon from "@mui/icons-material/HelpTwoTone";
import EditIcon from "@mui/icons-material/Edit";
import CloseIcon from "@mui/icons-material/Close";
import CheckIcon from "@mui/icons-material/Check";
import { toast } from "react-toastify";
import {
  getCloudinarySignature,
  uploadVideoToCloudinary,
} from "../../../services/cloudinaryService";
import type { UseFormSetValue, UseFormWatch } from "react-hook-form";
import type { InferType } from "yup";
import { editLessonSchema } from "../../../pages/Teacher/EditLesson/editLessonValidationSchema";
import CustomTextField from "../../../components/common/CustomTextField/CustomTextField";

type EditLessonFormInputs = InferType<typeof editLessonSchema>;

interface UploadVideoInputForEditProps {
  name: `videoUrls.${number}`;
  setValue: UseFormSetValue<EditLessonFormInputs>;
  watch: UseFormWatch<EditLessonFormInputs>;
  onRemove: () => void;
}

const UploadVideoInputForEdit: React.FC<UploadVideoInputForEditProps> = ({
  name,
  setValue,
  watch,
  onRemove,
}) => {
  const theme = useTheme();
  const [uploadProgress, setUploadProgress] = useState(0);
  const [uploading, setUploading] = useState(false);
  const [customFileName, setCustomFileName] = useState("");
  const [originalFileName, setOriginalFileName] = useState("");
  const [isEditingTitle, setIsEditingTitle] = useState(false);

  const uploadedUrl = watch(`${name}.url`);
  const savedFileName = watch(`${name}.title`);

  const handleFileChange = async (e: ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files[0]) {
      const file = e.target.files[0];
      setUploading(true);
      setOriginalFileName(file.name);

      try {
        const signature = await getCloudinarySignature(customFileName);

        const interval = setInterval(() => {
          setUploadProgress((prev) => Math.min(prev + 5, 95));
        }, 100);

        const uploadedUrl = await uploadVideoToCloudinary(
          file,
          signature,
          customFileName.trim()
        );

        clearInterval(interval);
        setUploadProgress(100);

        setValue(`${name}.url`, uploadedUrl, { shouldValidate: true });
        setValue(`${name}.title`, customFileName.trim() || file.name);

        toast.success("Video uploaded successfully");
      } catch (err) {
        toast.error("Video upload failed");
        console.error("Upload failed", err);
      } finally {
        setUploading(false);
        setTimeout(() => setUploadProgress(0), 1000);
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
        <Box
          sx={{ display: "flex", flexDirection: "column", maxWidth: "900px" }}
        >
          {originalFileName && (
            <Tooltip title={originalFileName}>
              <Typography variant="body2" sx={{ color: "#A5B1C2" }}>
                Original: {originalFileName}
              </Typography>
            </Tooltip>
          )}

          {!isEditingTitle ? (
            <Box sx={{ display: "flex", alignItems: "center",justifyContent:"space-between", gap: 1 }}>
              <Tooltip title={savedFileName || "No name set"}>
                <Typography variant="body2" sx={{ color: "#6EC76E" }}>
                  Named: {savedFileName || "—"}
                </Typography>
              </Tooltip>
              <IconButton
                onClick={(e) => {
                  e.stopPropagation();
                  setIsEditingTitle(true);
                }}
                sx={{
                  transition: "background-color 0.3s, color 0.3s",
                  "&:hover": {
                    backgroundColor: "#38424D",
                    "& svg": { color: "#FFD700" },
                  },
                }}
              >
                <EditIcon sx={{ color: theme.palette.text.primary }} />
              </IconButton>
            </Box>
          ) : (
            <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
              <CustomTextField
                label="Edit Video Name"
                size="small"
                value={customFileName}
                onChange={(e) => setCustomFileName(e.target.value)}
              />
              <IconButton
                onClick={(e) => {
                  e.stopPropagation();
                  setValue(`${name}.title`, customFileName.trim());
                  toast.success("Video name updated");
                  setIsEditingTitle(false);
                }}
                sx={{
                  transition: "background-color 0.3s, color 0.3s",
                  "&:hover": {
                    backgroundColor: "#38424D",
                    "& svg": { color: "#00B300" },
                  },
                }}
              >
                <CheckIcon sx={{ color: theme.palette.text.primary }} />
              </IconButton>

              <IconButton
                onClick={(e) => {
                  e.stopPropagation();
                  setIsEditingTitle(false);
                }}
                sx={{
                  transition: "background-color 0.3s, color 0.3s",
                  "&:hover": {
                    backgroundColor: "#38424D",
                    "& svg": { color: "#FF4C4C" },
                  },
                }}
              >
                <CloseIcon sx={{ color: theme.palette.text.primary }} />
              </IconButton>
            </Box>
          )}
        </Box>
      ) : !uploading ? (
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            gap: 1,
            py: 1,
            width: "100%",
          }}
        >
          <CustomTextField
            label="Video Name"
            size="small"
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
              }}
            >
              Upload Video
              <input
                type="file"
                accept="video/*"
                hidden
                onChange={handleFileChange}
              />
            </Button>

            <IconButton
              onClick={(e) => {
                e.stopPropagation();
                onRemove();
              }}
              sx={{
                "&:hover": {
                  backgroundColor: "#38424D",
                  "& svg": { color: "#FF4C4C" },
                },
              }}
            >
              <CancelTwoToneIcon sx={{ color: "#A5B1C2" }} />
            </IconButton>

            <Tooltip
              title={"No video selected. Adding a video is optional."}
              children={<HelpTwoToneIcon sx={{ color: "#A5B1C2", mr: 1 }} />}
            />
          </Box>
        </Box>
      ) : null}

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
              "& .MuiLinearProgress-bar": { backgroundColor: "#00B300" },
            }}
          />
          <Typography variant="body2">{uploadProgress}%</Typography>
        </Box>
      )}
    </Box>
  );
};

export default UploadVideoInputForEdit;
