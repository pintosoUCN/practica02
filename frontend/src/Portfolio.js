import React, { useEffect, useState } from 'react';
import axios from 'axios';
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
          <div className="row mt-4">
            <div className="col-md-4">
              <div className="card border border-3 border-primary">
                <img src={profileData.imageUrl} className="img-fluid rounded-start border border-1 border-primary" alt="mi foto" />
              </div>
            </div>
            <div className="col-md-8">
              <div className="card mb-5">
                <div className="card-body">
                  <h5 className="card-title">{`${profileData.name || ''} ${profileData.lastname || ''}`}</h5>
                  <p className="card-text">{profileData.summary || ''}</p>
                  <p className="card-text">
                    <small className="text-body-secondary">Precioso hombre de {profileData.age || '' } años</small>
                  </p>
                </div>
              </div>

              <section className="info">
                <ul className="list-group">
                  <li className="list-group-item active" aria-current="true">
                    Datos Personales
                  </li>
                  <li className="list-group-item">Edad: {profileData.age || ''} años</li>
                  <li className="list-group-item">Ciudad de origen: {profileData.city || ''}</li>
                  <li className="list-group-item">
                    Correo electrónico: <a href={`mailto:${profileData.email || ''}`}>{profileData.email || ''}</a>
                  </li>
                  <li className="list-group-item">
                    Instagram: <a href={profileData.instagram || ''}>{profileData.instagram || ''}</a>
                  </li>
                  <li className="list-group-item">
                    Facebook: <a href={profileData.facebook || ''}>{profileData.facebook || ''}</a>
                  </li>
                </ul>
              </section>
            </div>
          </div>

          <section className="intereses mt-4">
            <ul className="list-group">
              <li className="list-group-item list-group-item-success">Intereses</li>
              {profileData.hobbies?.map((hobby, index) => (
                <li className="list-group-item list-group-item-light" key={index}>
                  {hobby.description}
                </li>
              ))}
            </ul>
          </section>

          <section className="tecnologias mt-4">
            <h3>Herramientas Tecnológicas</h3>
            <table className="table table-striped table-hover mt-4">
              <thead>
                <tr>
                  <th>Nombre</th>
                  <th>Nivel</th>
                </tr>
              </thead>
              <tbody>
                {profileData.frameworks?.map((framework, index) => (
                  <tr key={index}>
                    <td>{framework.name}</td>
                    <td>
                      <div className="progress" role="progressbar" aria-label="Default striped example" aria-valuenow={framework.quantity} aria-valuemin="0" aria-valuemax="100">
                        <div className="progress-bar progress-bar-striped" style={{ width: `${framework.quantity}%` }}></div>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </section>
        </div>
      )}
    </div>
  );
};

export default Portfolio;
