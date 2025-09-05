import UniversalTabs from "../../components/common/UniversalTabs/UniversalTabs";
import TeacherFiltersPanel from "./TeacherFiltersPanel";

interface TeacherTabsProps {
  sort: string;
  setSort: (value: string) => void;
  tag: string | null;
  setTag: (tag: string | null) => void;
  onSearch: (query: string) => void;
}

const teacherTabs = [
  { label: "My Courses", route: "/app/teacher/my-courses" },
  { label: "Add Course", route: "/app/teacher/add-course" },
];

const TeacherTabs: React.FC<TeacherTabsProps> = ({
  sort,
  setSort,
  tag,
  setTag,
  onSearch,
}) => {
  return (
    <UniversalTabs
      tabs={teacherTabs}
      filtersPanel={(currentTab) =>
        currentTab.route === "/app/teacher/my-courses" ? (
          <TeacherFiltersPanel
            onSortChange={setSort}
            onTagChange={setTag}
            onSearch={onSearch}
          />
        ) : null
      }
    />
  );
};

export default TeacherTabs;
