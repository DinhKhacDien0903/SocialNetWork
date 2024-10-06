export const OPEN_CHAT = 'OPEN_CHAT';
export const CLOSE_CHAT = 'CLOSE_CHAT';

export const openChat = (payload) => {
    return {
        type: OPEN_CHAT,
        payload,
    };
};

export const closeChat = (payload) => {
    return {
        type: CLOSE_CHAT,
        payload,
    };
};
