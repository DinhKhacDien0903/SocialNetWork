import Sidebar from '~/components/Sidebar';

const DefaultLayout = ({ children }) => {
    return (
        <div className="d-flex">
            <Sidebar />
            <div style={{ marginLeft: '7.6rem', flex: 1 }}>{children}</div>
        </div>
    );
};

export default DefaultLayout;
