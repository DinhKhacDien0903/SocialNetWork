import ChatPopup from '~/components/ChatPopup';
import Post from '~/components/Post';

const Home = () => {
    const friendList = [
        {
            id: 'd6b95a50-23ed-4a7c-9b45-1dd6e3afb959',
            firstName: '6',
            lastName: '6',
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
