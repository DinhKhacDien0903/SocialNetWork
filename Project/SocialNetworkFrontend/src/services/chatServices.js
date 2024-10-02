import axios from '~/utils/axios';

export const getMessagesWithFriendService = (friendId) => {
    return axios.get(`/chat/messages?friendId=${friendId}`);
};

export const sendMessageWithFriendService = ({ friendId, message }) => {
    return axios.post('/chat/message', {
        friendId: friendId,
        message: message,
    });
};

export const createGroupChatService = ({ name, avatar, members }) => {
    return axios.post('/chat/group-chat', {
        name,
        avatar,
        members,
    });
};

export const getGroupChatsService = () => {
    return axios.get('/chat/group-chat');
};

export const getMessagesOfGroupChatService = (groupChatId) => {
    return axios.get(`/chat/group-chat/messages/${groupChatId}`);
};

export const sendGroupChatMessageService = ({ groupChatId, message, picture }) => {
    return axios.post('/chat/group-chat/message', {
        groupChatId,
        message,
        picture,
    });
};

export const getGroupMembersService = (groupId) => {
    return axios.get(`/chat/group-chat/members/${groupId}`);
};
