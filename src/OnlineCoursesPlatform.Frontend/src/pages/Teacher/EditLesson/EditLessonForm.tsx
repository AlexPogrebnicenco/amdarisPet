import { useEffect } from "react";
import { type Lesson } from "../../../services/lessonService";
import {
  Typography,
  Box,
  useTheme,
  IconButton,
  Tooltip,
  Divider,
} from "@mui/material";
import CustomTextField from "../../../components/common/CustomTextField/CustomTextField";
import AddCircleTwoToneIcon from "@mui/icons-material/AddCircleTwoTone";
import CancelTwoToneIcon from "@mui/icons-material/CancelTwoTone";
import CommonButton from "../../../components/common/CommonButton/CommonButton";
import DeleteVideoButton from "../../../components/common/DeleteVideoButton/DeleteVideoButton";

import { toast } from "react-toastify";
import { editLessonSchema } from "./editLessonValidationSchema";
import type { InferType } from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm, useFieldArray, Controller } from "react-hook-form";
import { updateLesson } from "../../../services/lessonService";
import { compare } from "fast-json-patch";
import UploadVideoInputForEdit from "./UploadVideoInputForEdit";
import TooltipModal from "../../../components/common/TooltipModal/TooltipModal";
import MDEditor from "@uiw/react-md-editor";

type EditLessonFormInputs = InferType<typeof editLessonSchema>;

interface EditLessonFormProps {
  lesson: Lesson;
}

const EditLessonForm: React.FC<EditLessonFormProps> = ({ lesson }) => {
  const theme = useTheme();

  const {
    register,
    control,
    handleSubmit,
    reset,
    setValue,
    watch,
    formState: { errors },
  } = useForm<EditLessonFormInputs>({
    resolver: yupResolver(editLessonSchema),
    defaultValues: {
      title: "",
      description: "",
      orderNumber: 1,
      content: "",
      resourcesArray: [{ url: "" }],
      videoUrls: [{ url: "", title: "" }],
    },
  });

  const { fields, append, remove } = useFieldArray({
    control,
    name: "resourcesArray",
  });

  const {
    fields: videoFields,
    append: appendVideo,
    remove: removeVideo,
  } = useFieldArray({
    control,
    name: "videoUrls",
  });

  useEffect(() => {
    reset({
      title: lesson.title,
      description: lesson.description,
      orderNumber: lesson.orderNumber,
      content: lesson.content || "",
      resourcesArray: (lesson.resources || []).map((url) => ({ url })),
      videoUrls: (lesson.videoUrls || []).map((video) => ({
        url: video.url,
        title: video.title,
      })),
    });
  }, [lesson, reset]);

  const extractPublicIdFromUrl = (url: string) => {
    const parts = url.split("/upload/");
    if (parts.length < 2) return "";
    const pathAfterUpload = parts[1].split(".mp4")[0];
    return pathAfterUpload.split("/").pop() || "";
  };

  const onSubmit = async (data: EditLessonFormInputs) => {
    if (!lesson) return;

    const original = {
      title: lesson.title,
      description: lesson.description,
      content: lesson.content ?? "",
      resources: lesson.resources ?? [],
      videoUrls: (lesson.videoUrls ?? []).map((v) => ({
        url: v.url,
        title: v.title ?? "",
      })),
    };

    const updated = {
      title: data.title,
      description: data.description,
      content: data.content ?? "",
      resources: (data.resourcesArray ?? []).map((r) => r.url?.trim() || ""),
      videoUrls: (data.videoUrls ?? []).map((v) => ({
        url: v.url?.trim() || "",
        title: v.title?.trim() || "",
      })),
    };

    const patch = compare(original, updated);

    if (patch.length === 0) {
      toast.info("No changes to save.");
      return;
    }

    try {
      await updateLesson(lesson.courseId, lesson.orderNumber, patch);
      toast.success("Lesson updated successfully!");
    } catch (error) {
      toast.error("Failed to update lesson.");
      console.error(error);
    }
  };

  return (
    <Box
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      sx={{
        display: "flex",
        flexDirection: "column",
        gap: 2,
        width: "100%",
        mx: "auto",
      }}
    >
      <Typography
        variant="h5"
        align="center"
        sx={{
          color: theme.palette.text.secondary,
          mb: 2,
        }}
      >
        Edit Lesson
      </Typography>

      <CustomTextField
        label="Lesson Title"
        {...register("title")}
        error={!!errors.title}
        helperText={errors.title?.message}
        required
      />

      <CustomTextField
        label="Lesson Description"
        {...register("description")}
        error={!!errors.description}
        helperText={errors.description?.message}
        multiline
        minRows={3}
        maxRows={15}
        required
      />

      <TooltipModal title="Order number cannot be changed. If you want to change it, please delete the lesson and create a new one.">
        <CustomTextField
          label="Order Number"
          type="number"
          {...register("orderNumber")}
          InputProps={{
            readOnly: true,
            sx: {
              pointerEvents: "none",
            },
          }}
          sx={{
            "& .MuiOutlinedInput-root": {
              "& fieldset": {
                border: "3px solid transparent",
                borderRadius: 2,
                background: `
                  linear-gradient(rgba(1, 11, 17, 0.1), rgba(2, 28, 42, 0.1)) padding-box,
                  linear-gradient(135deg, rgba(2, 28, 42, 0.1), #033d5b, rgba(2, 28, 42, 0.1)) border-box
                `,
                backgroundOrigin: "border-box",
                backgroundClip: "padding-box, border-box",
                p: 2,
                color: "#e0f7fa",
              },
            },
          }}
        />
      </TooltipModal>

      <Divider sx={{ mt: 1 }} />

      <Controller
        name="content"
        control={control}
        render={({ field }) => (
          <Box data-color-mode="dark">
            <Typography variant={"h6"} sx={{ my: 2, textAlign: "center" }}>
              Content
            </Typography>
            <Box
              sx={{
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
              <MDEditor
                value={field.value ?? ""}
                onChange={(val) => field.onChange(val || "")}
                minHeight={400}
                height={400}
              />
            </Box>
            {errors.content && (
              <Typography variant="caption" color="error">
                {errors.content.message}
              </Typography>
            )}
          </Box>
        )}
      />

      <Divider sx={{ mt: 1 }} />

      {/* Resources */}
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          gap: 2,
          mt: 2,
        }}
      >
        <Typography variant="h6">Resources</Typography>
        <IconButton
          onClick={(e) => {
            e.stopPropagation();
            append({ url: "" });
          }}
          sx={{
            transition: "background-color 0.3s, color 0.3s",
            "&:hover": {
              backgroundColor: "#38424D",
              "& svg": { color: "#00B300" },
            },
          }}
        >
          <AddCircleTwoToneIcon sx={{ color: theme.palette.text.primary }} />
        </IconButton>
      </Box>

      {fields.map((field, index) => (
        <Box
          key={field.id}
          sx={{ display: "flex", gap: 1, alignItems: "center" }}
        >
          <CustomTextField
            label={`${index + 1}. Resource URL(optional)`}
            {...register(`resourcesArray.${index}.url` as const)}
            fullWidth
          />
          <IconButton
            onClick={(e) => {
              e.stopPropagation();
              remove(index);
            }}
            sx={{
              transition: "background-color 0.3s, color 0.3s",
              "&:hover": {
                backgroundColor: "#38424D",
                "& svg": { color: "#FF4C4C" },
              },
            }}
          >
            <CancelTwoToneIcon sx={{ color: theme.palette.text.primary }} />
          </IconButton>
        </Box>
      ))}

      <Divider sx={{ mt: 1 }} />

      {/* Videos */}
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          gap: 2,
          mt: 2,
        }}
      >
        <Typography variant="h6">Video URLs</Typography>
        <IconButton
          onClick={(e) => {
            e.stopPropagation();
            appendVideo({ url: "", title: "" });
          }}
          sx={{
            transition: "background-color 0.3s, color 0.3s",
            "&:hover": {
              backgroundColor: "#38424D",
              "& svg": { color: "#00B300" },
            },
          }}
        >
          <AddCircleTwoToneIcon sx={{ color: theme.palette.text.primary }} />
        </IconButton>
      </Box>

      {videoFields.map((field, index) => {
        const currentVideoUrls = watch("videoUrls");
        const currentVideoUrl = currentVideoUrls?.[index]?.url;

        return (
          <Box key={field.id}>
            <Divider sx={{ my: 1 }} />
            <Box sx={{ display: "flex", gap: 1}}>
              <UploadVideoInputForEdit
                name={`videoUrls.${index}`}
                setValue={setValue}
                watch={watch}
                onRemove={() => removeVideo(index)}
              />
              {currentVideoUrl && (
                <Tooltip title="Delete from storage">
                  <span>
                    <DeleteVideoButton
                      videoUrl={currentVideoUrl}
                      onDelete={() => removeVideo(index)}
                      extractPublicIdFromUrl={extractPublicIdFromUrl}
                    />
                  </span>
                </Tooltip>
              )}
            </Box>
            <Divider sx={{ my: 1 }} />
          </Box>
        );
      })}

      <CommonButton
        type="submit"
        fullWidth
        sx={{
          maxWidth: "400px",
          mx: "auto",
          display: "block",
        }}
      >
        Save Changes
      </CommonButton>
    </Box>
  );
};

export default EditLessonForm;
