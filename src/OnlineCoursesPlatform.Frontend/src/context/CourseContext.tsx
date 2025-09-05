import { createContext, useContext, useState } from "react";
import type { ReactNode } from "react";

interface CourseContextType {
  courseId: number | null;
  setCourseId: (id: number) => void;
  resetCourseId: () => void; // 👈 добавляем метод сброса
}

const CourseContext = createContext<CourseContextType | undefined>(undefined);

export const CourseProvider = ({ children }: { children: ReactNode }) => {
  const [courseId, setCourseId] = useState<number | null>(null);

  const resetCourseId = () => setCourseId(null);

  return (
    <CourseContext.Provider value={{ courseId, setCourseId, resetCourseId }}>
      {children}
    </CourseContext.Provider>
  );
};

export const useCourse = () => {
  const context = useContext(CourseContext);
  if (!context) throw new Error("useCourse must be used within a CourseProvider");
  return context;
};
