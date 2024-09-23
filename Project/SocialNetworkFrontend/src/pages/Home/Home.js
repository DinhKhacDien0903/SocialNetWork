import Post from '~/components/Post';

const Home = () => {
    return (
        <div className="d-flex justify-content-center mt-5">
            <Post postInfo={{ id: 1 }} />
        </div>
    );
};

export default Home;
