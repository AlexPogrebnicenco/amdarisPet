import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { editCourseSchema } from "./editCourseValidationSchema";
import {
  Box,
  Typography,
  useTheme,
} from "@mui/material";
import type { InferType } from "yup";
import type { CourseDetails } from "../../../interfaces/CourseDto";
import CustomTextField from "../../../components/common/CustomTextField/CustomTextField";
import CommonButton from "../../../components/common/CommonButton/CommonButton";
import MDEditor from "@uiw/react-md-editor";
import { toast } from "react-toastify";
import { patchCourse } from "../../../services/courseService";
import { buildCoursePatch } from "./buildCoursePatch";

export type EditCourseFormInputs = InferType<typeof editCourseSchema>;

 interface EditCourseFormProps {
  course: CourseDetails;
  onSuccess?: () => void;
}

const EditCourseForm: React.FC<EditCourseFormProps> = ({
  course,
  onSuccess,
}) => {
  const theme = useTheme();

  const {
    register,
    control,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<EditCourseFormInputs>({
    resolver: yupResolver(editCourseSchema),
    defaultValues: {
      title: course.title,
      description: course.description,
      about: course.about ?? "",
    },
  });

  const onSubmit = async (data: EditCourseFormInputs) => {
    try {
      const patch = buildCoursePatch({
        title: data.title !== course.title ? data.title : undefined,
        description: data.description !== course.description ? data.description : undefined,
        about: data.about !== course.about ? data.about : undefined,
      });

      if (patch.length === 0) {
        toast.info("No changes made.");
        return;
      }

      await patchCourse(course.id, patch);

      toast.success("Course updated successfully!");
      if (onSuccess) onSuccess();
    } catch (error) {
      console.error("Failed to update course", error);
      toast.error("Failed to update course.");
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
      }}
    >
      <Typography variant="h5" align="center" sx={{ mb: 2 }}>
        Edit Course
      </Typography>

      <CustomTextField
        label="Title"
        {...register("title")}
        error={!!errors.title}
        helperText={errors.title?.message}
      />

      <CustomTextField
        label="Description"
        multiline
        minRows={3}
        maxRows={10}
        inputProps={{ maxLength: 4000 }}
        {...register("description")}
        error={!!errors.description}
        helperText={errors.description?.message}
      />

      <Controller
        name="about"
        control={control}
        render={({ field }) => (
          <Box data-color-mode="dark">
            <Typography variant="h6" sx={{ my: 2, textAlign: "center" }}>
              About the Course
            </Typography>
            <Box
              sx={{
                p: 1,
                borderRadius: 3,
                background: `linear-gradient(135deg, #021c2a 0%, #033d5b 25%, #022b3a 50%, #033d5b 75%, #010b11 100%)`,
                boxShadow: "0 8px 24px rgba(0, 0, 0, 0.5)",
              }}
            >
              <MDEditor
                value={field.value ?? ""}
                onChange={(val) => field.onChange(val || "")}
                height={400}
              />
            </Box>
            {errors.about && (
              <Typography variant="caption" color="error">
                {errors.about.message}
              </Typography>
            )}
          </Box>
        )}
      />

      <Box sx={{ display: "flex", justifyContent: "center", mt: 2 }}>
        <CommonButton type="submit" disabled={isSubmitting}>
          Save Changes
        </CommonButton>
      </Box>
    </Box>
  );
};

export default EditCourseForm;
