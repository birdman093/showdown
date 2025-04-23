import React, { useState } from 'react';
import Draft from '../Draft/Draft';
import { DraftService } from '../../services/draftService';

const DraftHome: React.FC = () => {
    const [draftId, setDraftId] = useState('');
    const [userId, setUserId] = useState('');
    const [isInDraft, setIsInDraft] = useState(false);
    const draftService = new DraftService();

    const handleCreateDraft = async () => {
        if (!draftId || !userId) {
            alert('Please enter both Draft ID and User ID');
            return;
        }

        try {
            await draftService.createDraft(draftId);
            setIsInDraft(true);
        } catch (error) {
            console.error('Failed to create draft:', error);
            alert('Failed to create draft');
        }
    };

    const handleJoinDraft = async () => {
        if (!draftId || !userId) {
            alert('Please enter both Draft ID and User ID');
            return;
        }

        try {
            await draftService.joinDraft(draftId, userId);
            setIsInDraft(true);
        } catch (error) {
            console.error('Failed to join draft:', error);
            alert('Failed to join draft');
        }
    };

    if (isInDraft) {
        return <Draft draftId={draftId} userId={userId} />;
    }

    return (
        <div className="draft-home">
            <h1>MLB Showdown Draft</h1>
            <div className="form-group">
                <input
                    type="text"
                    placeholder="Enter Draft ID"
                    value={draftId}
                    onChange={(e) => setDraftId(e.target.value)}
                />
            </div>
            <div className="form-group">
                <input
                    type="text"
                    placeholder="Enter User ID"
                    value={userId}
                    onChange={(e) => setUserId(e.target.value)}
                />
            </div>
            <div className="button-group">
                <button onClick={handleCreateDraft}>Create Draft</button>
                <button onClick={handleJoinDraft}>Join Draft</button>
            </div>
        </div>
    );
};

export default DraftHome;
