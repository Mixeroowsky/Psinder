const About = () => {
  return (
    <div className="flex p-10 justify-center ">
      <div className="p-5 max-w-screen-xl rounded-md border w-3/4">
        <h1>About</h1>
        <h3>Disclaimer:</h3>
        <p className="font-light text-justify text-base">
          This website has been developed exclusively as a demonstration of my
          skills and expertise in modern application development. It showcases
          my ability to design and implement solutions using technologies such
          as .NET, Entity Framework, React, and TypeScript. The project
          highlights my proficiency in building full-stack applications,
          integrating frontend and backend technologies seamlessly, and adhering
          to best practices in software development.
        </p>
        <br />
        <p className="font-light text-justify text-base">
          Please note that this site is not intended for commercial use or as a
          functional product. Its primary purpose is to serve as a portfolio
          entry and a means to present my technical knowledge, creativity, and
          problem-solving abilities in real-world scenarios. I hope it provides
          a clear example of my capabilities as a developer and my commitment to
          delivering high-quality software solutions.
        </p>
      </div>
    </div>
  );
};

export default About;
