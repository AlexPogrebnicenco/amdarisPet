import { useEffect, useState } from "react";
import {
  Box,
  IconButton,
  Paper,
  Typography,
  useTheme,
} from "@mui/material";
import CancelTwoToneIcon from "@mui/icons-material/CancelTwoTone";
import { AnimatePresence, motion } from "framer-motion";
import { useAuth } from "../../context/AuthContext";
import CommonButton from "../../components/common/CommonButton/CommonButton";
import { updateUserAccount } from "../../services/accountService";
import CustomTextField from "../../components/common/CustomTextField/CustomTextField";
import AvatarEditSection from "./AvatarEditSection";
import {
  getCloudinarySignature,
  uploadImageToCloudinary,
  deleteCloudinaryImage, 
} from "../../services/cloudinaryService";

interface EditAccountModalProps {
  open: boolean;
  onClose: () => void;
}

const EditAccountModal: React.FC<EditAccountModalProps> = ({
  open,
  onClose,
}) => {
  const theme = useTheme();
  const { userName, avatarUrl, setAuthState} = useAuth();

  const [formState, setFormState] = useState({
    userName: userName ?? "",
    avatarUrl: avatarUrl ?? "",
  });

  const [temporaryAvatarUrl, setTemporaryAvatarUrl] = useState<string | null>(
    null
  );
  const [uploading, setUploading] = useState(false);
  const [loading, setLoading] = useState(false);
  const [saved, setSaved] = useState(false); 

  // useEffect(() => {
  //   return () => {
  //     if (!saved && temporaryAvatarUrl && temporaryAvatarUrl !== avatarUrl) {
  //       const publicId = extractPublicId(temporaryAvatarUrl);
  //       if (publicId) {
  //         deleteCloudinaryImage(publicId).catch(console.error);
  //       }
  //     }
  //   };
  // }, [saved, temporaryAvatarUrl, avatarUrl]);

  useEffect(() => {
  if (open) {
    setFormState({
      userName: userName ?? "",
      avatarUrl: avatarUrl ?? "",
    });
    setTemporaryAvatarUrl(null);
    setSaved(false);
  }
}, [open]);

  const extractPublicId = (url: string) => {
    try {
      const matches = url.match(/\/v\d+\/([^\.\/]+)\./);
      return matches?.[1] || null;
    } catch {
      return null;
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormState((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      setLoading(true);
      const updated = await updateUserAccount(formState);
       setAuthState(localStorage.getItem("accessToken"), {
      userName: formState.userName,
      avatarUrl: updated.avatarUrl,
    });
      setSaved(true); 
      onClose();
    } catch (error) {
      console.error("Failed to update user:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleAvatarFileSelect = async (file: File) => {
    try {
      setUploading(true);
      const signature = await getCloudinarySignature("user-avatar", "image");
      const cloudUrl = await uploadImageToCloudinary(
        file,
        signature,
        "user-avatar"
      );
      setFormState((prev) => ({ ...prev, avatarUrl: cloudUrl }));
      setTemporaryAvatarUrl(cloudUrl);// используется для отката и удаления при закрытии
    } catch (error) {
      console.error("Cloudinary upload error", error);
    } finally {
      setUploading(false);
    }
  };

  const handleClose = () => {
  if (!saved && temporaryAvatarUrl && temporaryAvatarUrl !== avatarUrl) {
    const publicId = extractPublicId(temporaryAvatarUrl);
    if (publicId) {
      deleteCloudinaryImage(publicId).catch(console.error);
    }
  }
  onClose(); // вызываем оригинальный пропс onClose
};

  return (
    <AnimatePresence>
      {open && (
        <motion.div
          key="edit-account-modal"
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          exit={{ opacity: 0 }}
          transition={{ duration: 0.3 }}
          style={{
            position: "fixed",
            top: 0,
            left: 0,
            right: 0,
            bottom: 0,
            zIndex: 2000,
            backgroundColor: "rgba(0, 0, 0, 0.9)",
          }}
        >
          <motion.div
            initial={{ x: "100%", opacity: 0 }}
            animate={{ x: 0, opacity: 1 }}
            exit={{ x: "100%", opacity: 0 }}
            transition={{ duration: 0.3, ease: "easeInOut" }}
            style={{ height: "100%", overflowY: "auto" }}
          >
            <Box
              sx={{
                minHeight: "100%",
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                p: { xs: 2, sm: 4 },
              }}
            >
              <Paper
                elevation={6}
                sx={{
                  position: "relative",
                  p: { xs: 3, sm: 4 },
                  width: "100%",
                  maxWidth: "600px",
                  backgroundColor: theme.palette.background.paper,
                  borderRadius: 3,
                  boxShadow: "0 8px 24px rgba(0, 0, 0, 0.3)",
                }}
              >
                <IconButton
                  onClick={handleClose}
                  aria-label="Close modal"
                  sx={{
                    position: "absolute",
                    top: 16,
                    right: 16,
                    zIndex: 10,
                    transition: "background-color 0.3s, color 0.3s",
                    "&:hover": {
                      backgroundColor: "#38424D",
                      "& svg": { color: "#FF4C4C" },
                    },
                  }}
                >
                  <CancelTwoToneIcon
                    sx={{ color: "#A5B1C2", fontSize: "35px" }}
                  />
                </IconButton>

                <Typography
                  variant="h5"
                  gutterBottom
                  textAlign={"center"}
                  sx={{ color: theme.palette.text.secondary }}
                >
                  Edit Profile
                </Typography>

                <AvatarEditSection
                  avatarUrl={formState.avatarUrl}
                  onChange={handleAvatarFileSelect}
                />

                <Box component="form" onSubmit={handleSubmit} noValidate>
                  <CustomTextField
                    fullWidth
                    label="Username"
                    name="userName"
                    value={formState.userName}
                    onChange={handleChange}
                    margin="normal"
                  />

                  <Box
                    sx={{ mt: 2, display: "flex", justifyContent: "flex-end" }}
                  >
                    <CommonButton type="submit" disabled={loading}>
                      {loading ? "Saving..." : "Save Changes"}
                    </CommonButton>
                  </Box>
                </Box>
              </Paper>
            </Box>
          </motion.div>
        </motion.div>
      )}
    </AnimatePresence>
  );
};

export default EditAccountModal;
