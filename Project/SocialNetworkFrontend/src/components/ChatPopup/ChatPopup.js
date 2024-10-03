import { useCallback, useEffect, useRef, useState } from 'react';
import clsx from 'clsx';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronDown, faThumbsUp, faXmark } from '@fortawesome/free-solid-svg-icons';
import styles from './ChatPopup.module.scss';
import defaultAvatar from '~/assets/imgs/default-avatar.png';
import { useDispatch, useSelector } from 'react-redux';
import { userInfoSelector } from '~/redux/selectors';
import * as actions from '~/redux/actions';
import { getMessagesWithFriendService, sendMessageWithFriendService } from '~/services/chatServices';
import socket from '~/socket';
import _ from 'lodash';
import useClickOutside from '~/hook/useClickOutside';

import { HubConnectionBuilder } from '@microsoft/signalr';

// const conn = 

const initialListMessage = [
    {
        id: '67a43e79-1318-44ad-8303-f7a195795fae',
        sender: '18c713af-e5b7-4908-8a5b-ec84dd3b9fe2',
        receiver: 'e8490337-4a87-4665-b0d4-8fea750488ba',
        message: 'ê',
    },
    {
        id: 'f1ae90b2-9409-4fdc-afa0-b37c4726af03',
        sender: 'e8490337-4a87-4665-b0d4-8fea750488ba',
        receiver: '18c713af-e5b7-4908-8a5b-ec84dd3b9fe2',
        message: 'hello anh em nha mình đang bạn xíu tí mình vô nhé pp ae',
    },
    {
        id: '41663581-c79c-4014-8514-5308942a5280',
        sender: 'e8490337-4a87-4665-b0d4-8fea750488ba',
        receiver: '18c713af-e5b7-4908-8a5b-ec84dd3b9fe2',
        message: 'fasdfashdfasdfjasdfhasdfuyeiwthbdsghdsfkgdfg',
    },
    {
        id: '0d50ed85-3d16-4a98-8779-7cbbaec7c24b',
        sender: 'e8490337-4a87-4665-b0d4-8fea750488ba',
        receiver: '18c713af-e5b7-4908-8a5b-ec84dd3b9fe2',
        message: 'tyutyu',
    },
    {
        id: '0b3785fc-ee00-44cc-8041-884b50b02b32',
        sender: 'e8490337-4a87-4665-b0d4-8fea750488ba',
        receiver: '18c713af-e5b7-4908-8a5b-ec84dd3b9fe2',
        message: 'ghghj',
    },
    {
        id: '1c4a40fb-ebe7-42a6-8b77-32b44f75e3db',
        sender: 'e8490337-4a87-4665-b0d4-8fea750488ba',
        receiver: '18c713af-e5b7-4908-8a5b-ec84dd3b9fe2',
        message: 'gjhj',
    },
    {
        id: '07f59b7f-6bf6-4dc2-9f29-17611c4527fc',
        sender: '18c713af-e5b7-4908-8a5b-ec84dd3b9fe2',
        receiver: 'e8490337-4a87-4665-b0d4-8fea750488ba',
        message: 'tyutyu',
    },
];
const ChatPopup = ({ friend }) => {
    const { ref: chatPopupRef, isComponentVisible: isFocus, setIsComponentVisible: setIsFocus } = useClickOutside(true);
    //  const userInfo = useSelector(userInfoSelector);
    const userInfo = {
        id: '18c713af-e5b7-4908-8a5b-ec84dd3b9fe2',
    };

    const dispatch = useDispatch();

    const endOfMessagesRef = useRef(null);

    const [messages, setMessages] = useState([]);

    const [sendMessage, setSendMessage] = useState('');

    const [processingMessage, setProcessingMessage] = useState('');

    // const [messageList, setMessageList] = useState([]);

    const [conn, setConn] = useState('');
    useEffect(() => {
        setMessages(initialListMessage);
    },[]);

    useEffect(() => {
        const connection = new HubConnectionBuilder().withUrl('https://localhost:7072/chatPerson').build();
        // Mở kết nối
        const startConnection = async () => {
            try {
                await connection.start();
                console.log('Connection established');
                setConn(connection); 
            } catch (error) {
                console.error('Error establishing connection:', error);
            }
        };

        startConnection();

        return () => {
            if (connection) {
                connection.stop(); 
                console.log('Connection closed');
            }
        };
    }, []); 

    // setConnecion(conn);

    const sendMessageToPerson = async () => {
        console.log('Start chat >>>>>>' );
        try {

            const message = sendMessage;

            setSendMessage('');
            
            setMessages((prev) => {
                return [
                    ...prev,
                    {
                        id: null,
                        sender: userInfo?.id,
                        receiver: friend?.id,
                        message,
                    },
                ];
            })

            setProcessingMessage('Đang xử lý');

            // conn.on('ReceiveSpecitificMessage', ({ username, message, sendDate }) => {
            //     setMessages((prev) => [
            //         ...prev,
            //         {
            //             sender: userInfo?.id,
            //             message: sendMessage,
            //         },
            //     ]);
            //     console.log(`${username} has send ${message}`);
            // });

            var messageId = await conn.invoke('SendMessageToPerson', friend?.id, message );

            console.log('MessageId: ', messageId);

            setMessages((prev) => {
                const index = _.findIndex(prev, { id: null, message });

                if (index === -1) return prev;

                const updatedMessages = _.cloneDeep(prev);

                updatedMessages[index] = { ...updatedMessages[index], id: messageId };

                return updatedMessages;
            });

            setProcessingMessage('');

            console.log(`Sending message to ${friend?.id}: ${message}`);
        } catch (e) {
            console.log(e.message);
        }
    };

    useEffect(() => {
        endOfMessagesRef.current.scrollTop = endOfMessagesRef.current.scrollHeight;
    }, [messages]);

    const handleCloseChatPopup = useCallback(() => {
        dispatch(actions.closeChat(friend?.id));
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [friend?.id]);

    // const handleSendMessage = async () => {
    //     try {
    //         setMessages((prev) => [
    //             ...prev,
    //             {
    //                 id: null,
    //                 sender: userInfo?.id,
    //                 message: sendMessage,
    //             },
    //         ]);
    //         const clone = sendMessage;
    //         setSendMessage('');
    //         setProcessingMessage('Đang xử lý');
    //         const res = await sendMessageWithFriendService({ friendId: friend?.id, message: clone });
    //         setMessages((prev) => {
    //             const index = _.findIndex(prev, { id: null, message: clone });

    //             if (index === -1) return prev;

    //             const updatedMessages = _.cloneDeep(prev);
    //             updatedMessages[index] = { ...updatedMessages[index], id: res?.id };

    //             return updatedMessages;
    //         });
    //         setProcessingMessage('');
    //     } catch (error) {
    //         console.log(error);
    //         setProcessingMessage('Lỗi');
    //     }
    // };

    useEffect(() => {
        const handleNewMessage = (newMessage) => {
            if (newMessage.receiver === userInfo?.id) {
                setMessages((prev) => [
                    ...prev,
                    {
                        id: newMessage?.id,
                        sender: newMessage?.sender,
                        message: newMessage?.message,
                    },
                ]);
            }
        };
        // socket.on('newMessage', handleNewMessage);

        // return () => {
        //     socket.off('newMessage', handleNewMessage);
        // };
    }, [userInfo?.id]);

    useEffect(() => {
        window.onkeydown = (e) => {
            if (isFocus && e.key === 'Escape') {
                handleCloseChatPopup();
            }
        };
    }, [handleCloseChatPopup, isFocus]);

    const [showSetting, setShowSetting] = useState(false);
    const handleShowSetting = () => setShowSetting(true);
    const handleHideSetting = () => setShowSetting(false);

    return (
        <div className={clsx(styles['chat-wrapper'])} ref={chatPopupRef} onClick={() => setIsFocus(true)}>
            <div
                className={clsx(styles['chat-header'], {
                    [[styles['is-focus']]]: isFocus,
                })}
            >
                <div className={clsx(styles['chat-receiver'])}>
                    <div
                        className={clsx(styles['avatar'], {
                            [[styles['is-online']]]: friend?.isOnline,
                        })}
                    >
                        <img src={friend?.avatar || defaultAvatar} />
                    </div>
                    {friend?.lastName && friend?.firstName && (
                        <div className={clsx(styles['name'])}>{`${friend?.lastName} ${friend?.firstName}`}</div>
                    )}
                    <FontAwesomeIcon
                        className={clsx(styles['chat-setting'])}
                        icon={faChevronDown}
                        onClick={handleShowSetting}
                    />
                </div>
                <FontAwesomeIcon
                    icon={faXmark}
                    className={clsx(styles['chat-close'])}
                    onClick={() => handleCloseChatPopup(false)}
                />
            </div>
            <div ref={endOfMessagesRef} className={clsx(styles['chat-container'])}>
                {messages?.length > 0 ? (
                    messages?.map((message, index) => {
                        return (
                            <div
                                key={`chat-${index}`}
                                className={clsx(styles['message-wrapper'], {
                                    [[styles['message-current-user']]]: message?.sender === userInfo?.id,
                                })}
                            >
                                {messages[index - 1]?.sender !== message?.sender && message?.sender === friend?.id && (
                                    <img
                                        className={clsx(styles['message-avatar'])}
                                        src={friend?.avatar || defaultAvatar}
                                    />
                                )}
                                <div className={clsx(styles['message'])}>{message?.message}</div>
                                {processingMessage &&
                                    _.findLast(messages, { sender: userInfo?.id }) &&
                                    _.isEqual(_.findLast(messages, { sender: userInfo?.id }), message) && (
                                        <div className={clsx(styles['process-message'])}>{processingMessage}</div>
                                    )}
                            </div>
                        );
                    })
                ) : (
                    <div className="mt-5 text-center fz-16">
                        Hãy bắt đầu cuộc trò chuyện với {`${friend?.lastName} ${friend?.firstName}`}
                    </div>
                )}
                <div></div>
            </div>
            <div className={clsx(styles['chat-footer'])}>
                <div className={clsx(styles['send-message-wrapper'])}>
                    <input
                        value={sendMessage}
                        className={clsx(styles['send-message'])}
                        placeholder="Aa"
                        onChange={(e) => setSendMessage(e.target.value)}
                        onKeyDown={(e) => {
                            if (e.key === 'Enter') {
                                sendMessageToPerson();
                            }
                        }}
                    />
                    {sendMessage ? (
                        <i className={clsx(styles['send-message-btn'])} onClick={sendMessageToPerson}></i>
                    ) : (
                        <FontAwesomeIcon className={clsx(styles['link-icon'])} icon={faThumbsUp} />
                    )}
                </div>
            </div>
        </div>
    );
};

export default ChatPopup;
