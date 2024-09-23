import Sidebar from '~/components/Sidebar';

const DefaultLayout = ({ children }) => {
    return (
        <div className="d-flex">
            <Sidebar />
            <div style={{ marginLeft: '7.6rem' }}>{children}</div>
        </div>
    );
};

export default DefaultLayout;
