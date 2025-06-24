import HomeIcon from '@mui/icons-material/Home';
import SchoolIcon from '@mui/icons-material/School';
import TopicIcon from '@mui/icons-material/Topic';
import ExtensionIcon from '@mui/icons-material/Extension';
import EqualizerIcon from '@mui/icons-material/Equalizer';
import CardMembershipIcon from '@mui/icons-material/CardMembership';
import SettingsIcon from '@mui/icons-material/Settings';
import InfoIcon from '@mui/icons-material/Info';
import CoursesTabs from '../../../pages/Courses/CoursesTabs';

export const mainNavbarItems = [
    {
        id: 0,
        icon: <HomeIcon />,
        label: 'Home',
        route: 'home',
        description: 'Welcome to Online Courses Platform'
    },
    {
        id: 1,
        icon: <SchoolIcon />,
        label: 'Courses',
        route: 'courses',
        description: "Improve your web development skills in a variety of languages andframeworks, including HTML, CSS, JavaScript, React, TypeScript, andcutting-edge methods in AI.",
        tabsComponent: CoursesTabs
    },
    {
        id: 2,
        icon: <TopicIcon />,
        label: 'Topics',
        route: 'topics',
        description: 'Browse and explore individual topics to deepen your understanding and focus your learning.'
    },
    {
        id: 3,
        icon: <ExtensionIcon />,
        label: 'Projects',
        route: 'projects',
        description: 'Put your knowledge into practice by building real-world projects and portfolio pieces.'
    },
    {
        id: 4,
        icon: <EqualizerIcon />,
        label: 'Statistics',
        route: 'statistics',
        description: 'Track your progress, monitor learning streaks, and see detailed stats about your journey.'
    },
    {
        id: 5,
        icon: <CardMembershipIcon />,
        label: 'Subscription',
        route: 'subscription',
        description: 'Upgrade to premium for unlimited access, exclusive content, and advanced learning features.'
    },
    {
        id: 6,
        icon: <InfoIcon />,
        label: 'About Us',
        route: 'about-us',
        description: 'Learn more about our mission, our team, and the values that drive this platform forward.'
    }
];