import { useEffect, useState } from "react";
import { Controller, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { courseSchema } from "./courseValidationSchema";
import { Box, Typography, useTheme } from "@mui/material";
import CustomTextField from "../../../components/common/CustomTextField/CustomTextField";
import CommonButton from "../../../components/common/CommonButton/CommonButton";
import type { InferType } from "yup";
import {
  getAllCategories,
  type CategoryDto,
} from "../../../services/categoryService";
import { getAllTags, type TagDto } from "../../../services/tagService";
import { createCourse } from "../../../services/courseService";
import CustomAutocomplete from "../../../components/common/CustomAutocomplete/CustomAutocomplete";
import useMediaQuery from "@mui/material/useMediaQuery";
import { toast } from "react-toastify";
import MDEditor from "@uiw/react-md-editor";

type CourseFormInputs = InferType<typeof courseSchema>;

interface AddCourseFormProps {
  onCourseCreated?: () => void;
}

const difficultyOptions = [
  { label: "Beginner", value: "Beginner" },
  { label: "Intermediate", value: "Intermediate" },
  { label: "Advanced", value: "Advanced" },
];

const AddCourseForm: React.FC<AddCourseFormProps> = ({ onCourseCreated }) => {
  const theme = useTheme();
  const isMobile = useMediaQuery("(max-width:600px)");

  const {
    register,
    control,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm<CourseFormInputs>({
    resolver: yupResolver(courseSchema),
  });

  const [categories, setCategories] = useState<CategoryDto[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<CategoryDto | null>(
    null
  );

  const [tags, setTags] = useState<TagDto[]>([]);
  const [selectedTags, setSelectedTags] = useState<TagDto[]>([]);

  // Загрузка категорий и тегов
  useEffect(() => {
    const fetchData = async () => {
      try {
        const categories = await getAllCategories();
        setCategories(categories);

        const tags = await getAllTags();
        setTags(tags);
      } catch (error) {
        console.error("Error fetching categories or tags", error);
      }
    };

    fetchData();
  }, []);

  const filteredTags = tags.filter(
    (tag) => tag.categoryId === selectedCategory?.id
  );

  // Отправка формы
  const onSubmit = async (data: CourseFormInputs) => {
    try {
      if (!selectedCategory) {
        toast.warn("Please select a category.");
        return;
      }

      if (selectedTags.length === 0) {
        toast.warn("Please select at least one tag.");
        return;
      }

      await createCourse({
        ...data,
        about: data.about,
        categoryId: selectedCategory.id,
        tagIds: selectedTags.map((tag) => tag.id),
      });
      reset();
      setSelectedCategory(null);
      setSelectedTags([]);
      if (onCourseCreated) onCourseCreated();
      toast.success("Course created successfully!");
    } catch (error) {
      console.error("Error creating course", error);
      toast.error("Failed to create course.");
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
        Add New Course
      </Typography>

      <CustomTextField
        label="Course Title"
        {...register("title")}
        error={!!errors.title}
        helperText={errors.title?.message}
      />

      <CustomTextField
        label="Course Description"
        multiline
        minRows={3}
        maxRows={15}
        inputProps={{ maxLength: 4000 }}
        {...register("description")}
        error={!!errors.description}
        helperText={errors.description?.message}
      />

      {/* Селектор сложности */}
      <Controller
        control={control}
        name="difficulty"
        render={({ field }) => (
          <CustomAutocomplete
            options={difficultyOptions}
            getOptionLabel={(option) => option.label}
            value={
              difficultyOptions.find((opt) => opt.value === field.value) || null
            }
            onChange={(newValue) => field.onChange(newValue?.value || "")}
            label="Difficulty"
            placeholder="Select difficulty"
          />
        )}
      />

      {/* Селектор категорий */}
      <CustomAutocomplete<CategoryDto>
        options={categories}
        getOptionLabel={(option) => option.categoryName}
        value={selectedCategory}
        onChange={(newValue) => {
          setSelectedCategory(newValue);
          setSelectedTags([]);
        }}
        label="Select Category"
        placeholder="Category"
      />

      {/* Селектор тегов */}
      <CustomAutocomplete<TagDto, true>
        options={filteredTags}
        getOptionLabel={(option) => option.name}
        value={selectedTags}
        onChange={(newValue) => setSelectedTags(newValue)}
        label="Select Tags"
        placeholder="Tags"
        multiple
        disabled={!selectedCategory}
        listboxSx={{ maxHeight: isMobile ? "200px" : "350px" }}
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

      <Box
        sx={{ display: "flex", alignItems: "center", justifyContent: "center", mt:2 }}
      >
        <CommonButton type="submit">Create Course</CommonButton>
      </Box>
    </Box>
  );
};

export default AddCourseForm;
