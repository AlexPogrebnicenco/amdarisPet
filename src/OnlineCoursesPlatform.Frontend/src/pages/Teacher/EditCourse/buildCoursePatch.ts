import type { JsonPatchOperation } from "../../../interfaces/JsonPatchOperation";
import type { EditCourseFormInputs } from "./EditCourseForm"; 

export const buildCoursePatch = (
  data: Partial<EditCourseFormInputs>
): JsonPatchOperation[] => {
  const patch: JsonPatchOperation[] = [];

  if (data.title !== undefined) {
    patch.push({ op: "replace", path: "/title", value: data.title });
  }
  if (data.description !== undefined) {
    patch.push({ op: "replace", path: "/description", value: data.description });
  }
  if (data.about !== undefined) {
    patch.push({ op: "replace", path: "/about", value: data.about });
  }

  return patch;
};
