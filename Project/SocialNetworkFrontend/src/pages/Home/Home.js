import ChatPopup from '~/components/ChatPopup';
import Post from '~/components/Post';

const Home = () => {
    const friendList = [
        {
            id: '80fe9c6d-3b7b-44e0-a8bf-226d4c52384e',
            firstName: 'Trang',
            lastName: 'Dinh Thi',
            avatar: 'https://res.cloudinary.com/du19iyqz9/image/upload/v1727446956/file_1727446954211.jpg',
            isOnline: true,
        },
    ];
    return (
        <div className="d-flex justify-content-center mt-5">
            <Post postInfo={{ id: 1 }} />
            {friendList?.map((friend) => {
                return <ChatPopup key={`friend-${friend?.id}`} friend={friend} />;
            })}
        </div>
    );
};

export default Home;
