import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEnvelope, faMapMarkerAlt, faGlobe } from '@fortawesome/free-solid-svg-icons';
import 'bootstrap/dist/css/bootstrap.min.css';

const Portfolio = () => {
  const [profileData, setProfileData] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get('http://localhost:5065/api/profile');
        setProfileData(response.data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  }, []);

  return (
    <div className="container mt-5">
      {profileData && (
        <div>
          <div className="text-center">
            <h1 className="display-4">{`${profileData.Name || ''} ${profileData.Lastname || ''}`}</h1>
          </div>

          <div className="row mt-4">
            <div className="col-md-6">
              <h3>
                <FontAwesomeIcon icon={faEnvelope} /> Email
              </h3>
              <p>{profileData.email || ''}</p>

              <h3>
                <FontAwesomeIcon icon={faMapMarkerAlt} /> City
              </h3>
              <p>{profileData.city || ''}</p>

              <h3>
                <FontAwesomeIcon icon={faGlobe} /> Country
              </h3>
              <p>{profileData.country || ''}</p>

              <h3>Summary</h3>
              <p>{profileData.summary || ''}</p>

              <h3>Instagram</h3>
              <p>{profileData.instagram || ''}</p>

              <h3>Facebook</h3>
              <p>{profileData.facebook || ''}</p>

              <h3>Years Old</h3>
              <p>{profileData.yearsOld || ''}</p>
            </div>

            <div className="col-md-6">
              <h3>Frameworks</h3>
              <ul>
                {profileData.frameworks?.map((framework, index) => (
                  <li key={index}>
                    {framework.name} - Level: {framework.level}, Year: {framework.year}, Quantity: {framework.quantity}
                  </li>
                ))}
              </ul>

              <h3>Hobbies</h3>
              <ul>
                {profileData.hobbies?.map((hobby, index) => (
                  <li key={index}>
                    {hobby.name} - Description: {hobby.description}
                  </li>
                ))}
              </ul>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default Portfolio;
